namespace Infrastructure.Data.BoundedContext.ERPModule
{
    using Domain.BoundedContext.ERPModule;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.Data.Core;
    /// <summary>
    /// The Organization repository implementation
    /// </summary>
    public class OrganizationRepository
        : Repository<Organization>, IOrganizationRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        /// <param name="logger">Associated logger<param>
        public OrganizationRepository(IMainBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion
    }
}
