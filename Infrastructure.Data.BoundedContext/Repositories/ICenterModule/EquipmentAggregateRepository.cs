// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EquipmentAggregateRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The Address repository implementation
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Infrastructure.Data.BoundedContext.StoreModule
{
    using Domain.BoundedContext.StoreModule;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.Data.BoundedContext.Resources;
    using Infrastructure.Data.BoundedContext.UnitOfWork.Lucene;
    using Infrastructure.Data.Core;
    using System;
    using System.Linq;

    /// <summary>
    ///     The Address repository implementation
    /// </summary>
    public class EquipmentAggregateRepository : Repository<EquipmentAggregate>, IEquipmentAggregateRepository
    {
        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="AddressRepository"/> class. Create a new instance</summary>
        /// <param name="unitOfWork">Associated unit Of Work</param>
        /// <param name="logger">Associated logger</param>
        public EquipmentAggregateRepository(ILuceneBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }



        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public override void Add(EquipmentAggregate item)
        {
            if (item == (EquipmentAggregate)null)
                throw new ArgumentNullException(typeof(EquipmentAggregate).ToString(), Messages.CannotAddNullEntity);
            var UofW = this.UnitOfWork as ILuceneBCUnitOfWork;
            UofW.Equipments.Add(item);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public override EquipmentAggregate GetElementById(Guid Id)
        {
            var UofW=this.UnitOfWork as IQueryableUnitOfWork;
           var query= UofW.CreateSet<EquipmentAggregate>();

           return query.FirstOrDefault(x => x.Id == Id);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public override IQueryable<EquipmentAggregate> GetAllElements()
        {
            var UofW = this.UnitOfWork as IQueryableUnitOfWork;

            return UofW.CreateSet<EquipmentAggregate>();
        }

        #endregion
    }
}