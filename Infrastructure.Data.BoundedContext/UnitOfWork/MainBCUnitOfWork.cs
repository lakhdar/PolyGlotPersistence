using Domain.BoundedContext.ERPModule;
using Domain.BoundedContext.MembershipModule;
using Domain.Core;
using Infrastructure.Data.BoundedContext.Resources;
using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    public class MainBCUnitOfWork : DbContext, IMainBCUnitOfWork 
    {
        #region Fileds
        private IDbSet<Customer> _customers;
        private IDbSet<Address> _addresses;
        private IDbSet<Department> _departments;
        private IDbSet<Organization> _organizations;
        private IDbSet<Position> _positions;
        private IDbSet<User> _users;
        private IDbSet<Role> _roles;
        private IDbSet<Login> _logins;
        private IDbSet<Claim> _userClaims;
        #endregion

        #region Properties
        public IDbSet<Customer> Customers
        {
            get
            {
                if (this._customers == null)
                    this._customers = (IDbSet<Customer>)this.Set<Customer>();
                return this._customers;
            }
        }

        public IDbSet<Address> Addresses
        {
            get
            {
                if (this._addresses == null)
                    this._addresses = (IDbSet<Address>)this.Set<Address>();
                return this._addresses;
            }
        }

        public IDbSet<Department> Departments
        {
            get
            {
                if (this._departments == null)
                    this._departments = (IDbSet<Department>)this.Set<Department>();
                return this._departments;
            }
        }



        public IDbSet<Organization> Organizations
        {
            get
            {
                if (this._organizations == null)
                    this._organizations = (IDbSet<Organization>)this.Set<Organization>();
                return this._organizations;
            }
        }

        public IDbSet<Position> Positions
        {
            get
            {
                if (this._positions == null)
                    this._positions = (IDbSet<Position>)this.Set<Position>();
                return this._positions;
            }
        }

        public IDbSet<User> Users
        {
            get
            {
                if (this._users == null)
                    this._users = (IDbSet<User>)this.Set<User>();
                return this._users;
            }
        }

        public IDbSet<Role> Roles
        {
            get
            {
                if (this._roles == null)
                    this._roles = (IDbSet<Role>)this.Set<Role>();
                return this._roles;
            }
        }

        public IDbSet<Login> Logins
        {
            get
            {
                if (this._logins == null)
                    this._logins = (IDbSet<Login>)this.Set<Login>();
                return this._logins;
            }
        }

        public IDbSet<Claim> UserClaims
        {
            get
            {
                if (this._userClaims == null)
                    this._userClaims = (IDbSet<Claim>)this.Set<Claim>();
                return this._userClaims;
            }
        }

        #endregion

        #region IQueryableUnitOfWork
        public virtual IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class,new()
        {
            return (IDbSet<TEntity>)this.Set<TEntity>();
        }

        public virtual void Attach<TEntity>(TEntity item) where TEntity : class
        {
            this.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public virtual void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            this.Entry<TEntity>(item).State = EntityState.Modified;
        }

        public virtual void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            this.Entry<TEntity>(original).CurrentValues.SetValues((object)current);
        }

        #endregion

        #region IUnitOfWork
        public virtual void Commit()
        {
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw this.GetDBValidationExptions(ex);
            }
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                await this.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                throw this.GetDBValidationExptions(ex);
            }
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var entry in this.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                        {
                            entry.State = EntityState.Detached;
                        }
                    }

                    throw GetDBValidationExptions(dbEx);
                    
                }
            } while (saveFailed);
        }

        public async Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            bool saveFailed = false;

            do
            {
                try
                {
                    await base.SaveChangesAsync(cancellationToken);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var entry in this.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                        {
                            entry.State = EntityState.Detached;
                        }
                    }

                    throw GetDBValidationExptions(dbEx);
                }

            } while (saveFailed);
        }

        public void RollbackChanges()
        {
             this.ChangeTracker.Entries().ToList().ForEach((entry => entry.State = EntityState.Unchanged));
        }

        #endregion

        #region ISsql
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return (IEnumerable<TEntity>)this.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion

        #region DbContext
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }

        #endregion

        #region local
        private Exception GetDBValidationExptions(DbEntityValidationException dbEx)
        {
            string message = string.Empty;
            foreach (DbEntityValidationResult validationResult in dbEx.EntityValidationErrors)
            {
                foreach (DbValidationError dbValidationError in (IEnumerable<DbValidationError>)validationResult.ValidationErrors)
                    message = message + string.Format("Property: {0} Error: {1}", (object)dbValidationError.PropertyName, (object)dbValidationError.ErrorMessage);
            }
            return new Exception(Messages.ValidationError, new Exception(message));
        }

        #endregion
    }
}
