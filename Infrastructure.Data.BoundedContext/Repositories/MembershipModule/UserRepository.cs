namespace Infrastructure.Data.BoundedContext.MembershipModule.Repositories
{
    using Domain.BoundedContext.MembershipModule;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.Data.Core;
    /// <summary>
    /// The User repository implementation
    /// </summary>
    public class UserRepository
        : Repository<User>, IUserRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        /// <param name="logger">Associated logger</param>
        public UserRepository(IMainBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
            
        }

        #endregion
    }
}
