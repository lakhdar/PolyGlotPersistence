
namespace Application.BoundedConext.StoreModule
{
    using Domain.BoundedContext.StoreModule;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
   public interface IEquipmentAggregateManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       Task AddEquipmentAsync(EquipmentAggregate equipmentAggregate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
       Task<IEnumerable<EquipmentAggregate>> GetAllEquipmentAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
