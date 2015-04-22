namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    using Domain.BoundedContext.ERPModule;
    using Domain.BoundedContext.MembershipModule;
    using Domain.Core;
    using Infrastructure.Data.Core;
    using System;
    using System.Data.Entity;

    public interface IMainBCUnitOfWork : IQueryableUnitOfWork, IUnitOfWork, IDisposable, ISql
    {
        IDbSet<Customer> Customers { get; }

        IDbSet<Address> Addresses { get; }

        IDbSet<Department> Departments { get; }

        IDbSet<Organization> Organizations { get; }

        IDbSet<Position> Positions { get; }

        IDbSet<User> Users { get; }

        IDbSet<Role> Roles { get; }

        IDbSet<Login> Logins { get; }

        IDbSet<Claim> UserClaims { get; }
       
    }
}
