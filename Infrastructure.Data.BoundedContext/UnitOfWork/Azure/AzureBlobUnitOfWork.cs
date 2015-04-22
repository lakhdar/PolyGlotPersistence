namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    using Domain.BoundedContext.BlobAggregates;
    using Infrastructure.Data.Core;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    public class AzureBlobUnitOfWork : IAzureBlobUnitOfWork
    {
        #region Field
        private MemorySet<BlobAggregate> _blobAggregates;

        #endregion


        #region Prperties
        public IDbSet<BlobAggregate> BlobAggregates
        {
            get
            {
                if (this._blobAggregates == null)
                {
                    this._blobAggregates = new MemorySet<BlobAggregate>();
                }
                return this._blobAggregates;
            }
        }

        #endregion


        #region IQueryableUnitOfWork
        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class,new()
        {
            IDbSet<TEntity> dbSet = (IDbSet<TEntity>)null;
            string name = typeof(TEntity).Name;
            PropertyInfo property = this.GetType().GetProperty(name.EndsWith("s") ? name : name + "s");
            if (property == (PropertyInfo)null)
                property = this.GetType().GetProperty(typeof(TEntity).Name + "es");
            if (property != (PropertyInfo)null)
                dbSet = (IDbSet<TEntity>)property.GetValue((object)this, (object[])null);
            return dbSet;
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            List<ValidationResult> list = new List<ValidationResult>();
            ValidationContext validationContext = new ValidationContext(item, null, null);
            if (!Validator.TryValidateObject(item, validationContext, list))
            {
                string errors = list.Aggregate("", (x, c) => x + "  " + c.ErrorMessage);
                throw new Exception(errors);
            }
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            original = current;
        }

        #endregion

        #region IUnitOfWork
        public void Commit()
        {
            var groupes = BlobAggregates.GroupBy(x => x.ContainerName);
            foreach (var grp in groupes)
            {
                CloudBlobContainer container = this.GetContainer(grp.Key);
                foreach (var blob in grp)
                {
                    this.UploadBlob(blob, container);
                }
            }
        }

        public void CommitAndRefreshChanges()
        {
        }

        public void RollbackChanges()
        {

        }

        public async Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var groupes = BlobAggregates.GroupBy(x => x.ContainerName);
            foreach (var grp in groupes)
            {
                string containerName =string.IsNullOrEmpty(grp.Key)? CloudConfigurationManager.GetSetting("StaticContent.Container"):grp.Key;
                CloudBlobContainer container = this.GetContainer(grp.Key);
                foreach (var blob in grp)
                {
                    await this.UploadBlobAsync(blob, container);
                }
            }

        }

        public async Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Task.Run((Action)(() => { }));
        }

        #endregion
        #region ISsql
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return (IEnumerable<TEntity>)null;
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return 0;
        }

        #endregion

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion
        #region local
        /// <summary>
        /// Upload the files in the folder
        /// </summary>
        /// <param name="folderName">Folder in web application project to upload files from</param>
        /// <param name="container">Destination BLOB Storage container to upload files to</param>
        private void UploadBlob(BlobAggregate file, CloudBlobContainer container)
        {
            string reference = string.Format("{0}/{1}", file.DirectoryName, file.FileName);

            var blobFile = container.GetBlockBlobReference(reference);
            if (!blobFile.Exists())
            {
                blobFile.UploadFromStream(file.Content);
            }
        }

        /// <summary>
        /// Uploadasync the files in the folder
        /// </summary>
        /// <param name="folderName">Folder in web application project to upload files from</param>
        /// <param name="container">Destination BLOB Storage container to upload files to</param>
        private async Task UploadBlobAsync(BlobAggregate file, CloudBlobContainer container)
        {
            string reference = string.Format("{0}/{1}", file.DirectoryName, file.FileName);

            var blobFile = container.GetBlockBlobReference(reference);
            if (!blobFile.Exists())
            {
                await blobFile.UploadFromStreamAsync(file.Content);
            }
        }


        /// <summary>
        /// Get's blobContainer according to config settings
        /// </summary>
        private CloudBlobContainer GetContainer(string containerName)
        {
         
            string connectionString = CloudConfigurationManager.GetSetting("StaticContent.StorageConnectionString");

            var account = CloudStorageAccount.Parse(connectionString);
            var blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer contentContainer = blobClient.GetContainerReference(containerName);
            contentContainer.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            return contentContainer;
        }

        #endregion
    }
}
