using Domain.BoundedContext.ERPModule;
using Infrastructure.Data.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.BoundedContext.UnitOfWork.Mongo
{
    public interface IMongoUnitOfWork : IQueryableUnitOfWork
    {
        IDbSet<DepartmentAggregate> Departments { get; } 

    }
}
