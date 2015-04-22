namespace Domain.BoundedContext.BlobAggregates
{
    using Domain.Core;
    using System.IO;
    public class BlobAggregate : EntityBase
    {
        /// <summary>
        /// Get or set  blob directory name   
        /// </summary>
        public string DirectoryName { get; set; }

        /// <summary>
        /// Get or set  blob file name   
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Get or set  blob Container name   
        /// </summary>
        public string ContainerName { get; set; }

        /// <summary>
        /// Get or set  the  Content  
        /// </summary>
        public Stream Content { get; set; }

    }
}
