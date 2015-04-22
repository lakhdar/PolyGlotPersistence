namespace Application.BoundedConext.StoreModule
{
    using Domain.BoundedContext.StoreModule;
    using Infrastructure.CrossCutting.Core;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    public class EquipmentAggregateManagementService : IEquipmentAggregateManagementService
    {

        private IEquipmentAggregateRepository _equipmentAggregateRepository;
        private ILogger _logger;

        public EquipmentAggregateManagementService(IEquipmentAggregateRepository equipmentAggregateRepository, ILogger logger)
        {
            if (equipmentAggregateRepository == (IEquipmentAggregateRepository)null)
                throw new ArgumentNullException("equipmentAggregateRepository");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");
            this._equipmentAggregateRepository = equipmentAggregateRepository;
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organization"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AddEquipmentAsync(EquipmentAggregate equipmentAggregate, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (equipmentAggregate == (EquipmentAggregate)null)
                throw new ArgumentNullException("equipmentAggregate");
            _equipmentAggregateRepository.Add(equipmentAggregate);
            return Task.Run(() =>
            {
                _equipmentAggregateRepository.UnitOfWork.Commit();
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IEnumerable<EquipmentAggregate>> GetAllEquipmentAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<IEnumerable<EquipmentAggregate>>(this._equipmentAggregateRepository.GetAllElements());
        }
    }
}
