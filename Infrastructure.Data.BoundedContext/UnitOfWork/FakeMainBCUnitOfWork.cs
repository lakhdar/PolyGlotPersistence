namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Domain.BoundedContext.ERPModule;
    using Domain.BoundedContext.MembershipModule;
    using Domain.Core;

    using Infrastructure.Data.BoundedContext.UnitOfWork.Lucene;
    using Infrastructure.Data.Core;
    using System.Threading;

    public class FakeMainBCUnitOfWork : IMainBCUnitOfWork
    {
        #region Field

        private InitialMemorySet data = new InitialMemorySet();
        private MemorySet<Customer> _customers;
        private MemorySet<Address> _addresses;
        private MemorySet<Department> _departments;
        private MemorySet<Organization> _organizations;
        private MemorySet<Position> _positions;
        private MemorySet<User> _users;
        private MemorySet<Role> _roles;
        private MemorySet<Login> _logins;
        private MemorySet<Claim> _userClaims;

        #endregion

        #region Properties
        public IDbSet<Customer> Customers
        {
            get
            {
                if (this._customers == null)
                    this._customers = new MemorySet<Customer>((IEnumerable<Customer>)this.data.Customers);
                return (IDbSet<Customer>)this._customers;
            }
        }

        public IDbSet<Address> Addresses
        {
            get
            {
                if (this._addresses == null)
                    this._addresses = new MemorySet<Address>((IEnumerable<Address>)this.data.Addresses);
                return (IDbSet<Address>)this._addresses;
            }
        }

        public IDbSet<Department> Departments
        {
            get
            {
                if (this._departments == null)
                    this._departments = new MemorySet<Department>((IEnumerable<Department>)this.data.Departments);
                return (IDbSet<Department>)this._departments;
            }
        }


        public IDbSet<Organization> Organizations
        {
            get
            {
                if (this._organizations == null)
                    this._organizations = new MemorySet<Organization>((IEnumerable<Organization>)this.data.Organizations);
                return (IDbSet<Organization>)this._organizations;
            }
        }

        public IDbSet<Position> Positions
        {
            get
            {
                if (this._positions == null)
                    this._positions = new MemorySet<Position>((IEnumerable<Position>)this.data.Positions);
                return (IDbSet<Position>)this._positions;
            }
        }

        public IDbSet<User> Users
        {
            get
            {
                if (this._users == null)
                    this._users = new MemorySet<User>((IEnumerable<User>)this.data.Users);
                return (IDbSet<User>)this._users;
            }
        }

        public IDbSet<Role> Roles
        {
            get
            {
                if (this._roles == null)
                    this._roles = new MemorySet<Role>(this.data.Roles);
                return (IDbSet<Role>)this._roles;
            }
        }

        public IDbSet<Login> Logins
        {
            get
            {
                if (this._logins == null)
                    this._logins = new MemorySet<Login>(this.data.Logins);
                return (IDbSet<Login>)this._logins;
            }
        }

        public IDbSet<Claim> UserClaims
        {
            get
            {
                if (this._userClaims == null)
                    this._userClaims = new MemorySet<Claim>(this.data.Claims);
                return (IDbSet<Claim>)this._userClaims;
            }
        }

        #endregion

        #region IQueryableUnitOfWork
        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class,new()
        {
            IDbSet<TEntity> dbSet = (IDbSet<TEntity>)null;
            string name = typeof(TEntity).Name;
            PropertyInfo property = this.GetType().GetProperty(name.EndsWith("s") ? name : name + "s");
            if (property == (PropertyInfo)null)
                property = this.GetType().GetProperty(typeof(TEntity).Name + "es");
            if (property != (PropertyInfo)null)
                dbSet = (IDbSet<TEntity>)property.GetValue((object)this, (object[])null);
            return dbSet;
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            List<ValidationResult> list = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(item, null, null);
            if (!Validator.TryValidateObject(item, validationContext, list))
            {
                string errors = list.Aggregate("", (x, c) => x + "  " + c.ErrorMessage);
                throw new Exception(errors);
            }
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            original = current;
        }

        #endregion

        #region IUnitOfWork
        public void Commit()
        {
        }

        public void CommitAndRefreshChanges()
        {
        }

        public void RollbackChanges()
        {
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run((Action)(() => { }));
        }

        public async Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run((Action)(() => { }));
        }

        #endregion

        #region ISsql
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return (IEnumerable<TEntity>)null;
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return 0;
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
