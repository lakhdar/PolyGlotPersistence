using Domain.BoundedContext.BlobAggregates;
using Infrastructure.CrossCutting.Core;
using Infrastructure.Data.BoundedContext.UnitOfWork;
using Infrastructure.Data.Core;
namespace Infrastructure.Data.BoundedContext.BlobAggregates
{
    
    /// <summary>
    /// The Comment repository implementation
    /// </summary>
    public class BlobAggregateRepository
        : Repository<BlobAggregate>, IBlobAggregateRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        /// <param name="logger">Associated logger</param>
        public BlobAggregateRepository(IAzureBlobUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion
    }
}
