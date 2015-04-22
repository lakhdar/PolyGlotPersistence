namespace Infrastructure.Data.BoundedContext.Repositories.ERPModule
{
    using Domain.BoundedContext.ERPModule;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.Data.BoundedContext.Resources;
    using Infrastructure.Data.BoundedContext.UnitOfWork.Mongo;
    using Infrastructure.Data.Core;
    using System;
    using System.Linq;
    public class DepartmentAggregateRepository : Repository<DepartmentAggregate>, IDepartmentAggregateRepository
    {

        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
         /// <param name="logger">Associated logger</param>
        public DepartmentAggregateRepository(IMongoUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }


        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public override void Add(DepartmentAggregate item)
        {
            if (item == (DepartmentAggregate)null)
                throw new ArgumentNullException(typeof(DepartmentAggregate).ToString(), Messages.CannotAddNullEntity);
            var UofW = this.UnitOfWork as IMongoUnitOfWork;
            UofW.Departments.Add(item);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public override DepartmentAggregate GetElementById(Guid Id)
        {
            var UofW = this.UnitOfWork as IQueryableUnitOfWork;
            var query = UofW.CreateSet<DepartmentAggregate>();

            return query.FirstOrDefault(x => x.Id == Id);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public override IQueryable<DepartmentAggregate> GetAllElements()
        {
            var UofW = this.UnitOfWork as IQueryableUnitOfWork;

            return UofW.CreateSet<DepartmentAggregate>();
        }

        #endregion
    }
}
