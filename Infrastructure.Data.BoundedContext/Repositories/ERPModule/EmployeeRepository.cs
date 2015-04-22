namespace Infrastructure.Data.BoundedContext.ERPModule
{
    using Domain.BoundedContext.ERPModule;
    using Infrastructure.Data.Core;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using Infrastructure.CrossCutting.Core;
    /// <summary>
    /// The Employee repository implementation
    /// </summary>
    public class EmployeeRepository
        : Repository<Employee>, IEmployeeRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        /// <param name="logger">Associated logger</param>
        public EmployeeRepository(IMainBCUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion
    }
}
