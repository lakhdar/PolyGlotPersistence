namespace Application.BoundedConext.Blob
{
    using System.Threading.Tasks;
    using Domain.BoundedContext.BlobAggregates;
    using Infrastructure.CrossCutting.Core;
    using System;
    using Infrastructure.Data.BoundedContext.UnitOfWork;
    using System.Collections.Generic;

   public class BlobManagementServices:IBlobManagementServices
   {


       #region Fields
       private IBlobAggregateRepository _blobRepository;
        private ILogger _logger;
       #endregion

        #region Ctor
        public BlobManagementServices(IBlobAggregateRepository blobRepository, ILogger logger)
        {
            if (blobRepository == (IBlobAggregateRepository)null)
                throw new ArgumentNullException("blobRepository");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");
            this._blobRepository = blobRepository;
            this._logger = logger;
        }

        #endregion



        #region IBlobManagementServices

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="file"><see cref="Application.BoundedConext.Blob"/></param>
        public Task UploadBlobAsync(BlobAggregate file)
        {

            this._blobRepository.Add(file);

            return this._blobRepository.UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="files"><see cref="Application.BoundedConext.Blob"/></param>
        public Task UploadBlobAsync(IEnumerable<BlobAggregate> files)
        {
            foreach (var file in files)
            {
                this._blobRepository.Add(file);
            }

            return this._blobRepository.UnitOfWork.CommitAsync();
        }



        #endregion
   }
}
