namespace Infrastructure.Data.BoundedContext.MembershipModule 
{
    using Domain.BoundedContext.MembershipModule;
    using Infrastructure.Data.Core;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.CrossCutting.Core;
    /// <summary>
    /// The User repository implementation
    /// </summary>
    public class RoleRepository
        : Repository<Role>, IRoleRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        /// <param name="logger">Associated logger</param>
        public RoleRepository(IMainBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion
    }
}
