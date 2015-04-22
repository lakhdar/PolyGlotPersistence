namespace Domain.BoundedContext.StoreModule
{
    using Domain.Core;

    /// <summary>
    /// Customer repository contract
    /// <see cref="Domain.Core.IRepository{EquipmentAggregate}"/>
    /// </summary>
    public interface IEquipmentAggregateRepository : IRepository<EquipmentAggregate>
    {
    }
}
