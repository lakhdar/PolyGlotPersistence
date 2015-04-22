namespace Application.BoundedConext.Blob
{
    using Domain.BoundedContext.BlobAggregates;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IBlobManagementServices
    {

        /// <summary>
        /// Upload a file to blob storage as blob aggregate
        /// </summary>
        /// <param name="file">a file as blobAggregate</param>
        /// <returns></returns>
        Task UploadBlobAsync(BlobAggregate file);


        /// <summary>
        /// Upload a file to blob storage as blob aggregate
        /// </summary>
        /// <param name="files">a collection of  files as blobAggregate to upload to storage</param>
        /// <returns></returns>
        Task UploadBlobAsync(IEnumerable<BlobAggregate> files);

    }
}
