namespace Infrastructure.Data.BoundedContext.ERPModule
{
    using Domain.BoundedContext.ERPModule;
    using Infrastructure.Data.Core;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.CrossCutting.Core;
    /// <summary>
    /// The Address repository implementation
    /// </summary>
    public class AddressRepository
        : Repository<Address>, IAddressRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit Of Work</param>
        /// <param name="logger">Associated logger</param>
        public AddressRepository(IMainBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion
    }
}
