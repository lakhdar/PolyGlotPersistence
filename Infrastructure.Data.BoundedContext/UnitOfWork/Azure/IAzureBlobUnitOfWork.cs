
namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    using Domain.BoundedContext.BlobAggregates;
    using Domain.Core;
    using Infrastructure.Data.Core;
    using System;
    using System.Data.Entity;

    public interface IAzureBlobUnitOfWork : IQueryableUnitOfWork, IUnitOfWork, IDisposable
    {
        IDbSet<BlobAggregate> BlobAggregates { get; }
    }
}
