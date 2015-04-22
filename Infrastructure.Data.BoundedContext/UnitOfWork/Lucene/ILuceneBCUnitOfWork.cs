namespace Infrastructure.Data.BoundedContext.UnitOfWork.Lucene
{
    using System.Collections.Generic;
    using System.Data.Entity;

    using Domain.BoundedContext.StoreModule;

    using Infrastructure.Data.Core;
    using global::Lucene.Net.Linq;

    /// <summary>The LuceneBCUnitOfWork interface.</summary>
    public interface ILuceneBCUnitOfWork : IQueryableUnitOfWork
    {
        LuceneDataProvider DataProvider { get; }
        IDbSet<EquipmentAggregate> Equipments { get; }
    }
}
