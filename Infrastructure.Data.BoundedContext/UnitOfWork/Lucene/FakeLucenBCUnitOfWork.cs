namespace Infrastructure.Data.BoundedContext.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Domain.BoundedContext.ERPModule;

    using Infrastructure.Data.BoundedContext.UnitOfWork.Lucene;
    using Infrastructure.Data.Core;
    using System.Linq;
    using global::Lucene.Net.Linq;
    using global::Lucene.Net.Store;
    using global::Lucene.Net.Index;
    using Version = global::Lucene.Net.Util.Version;
    using global::Lucene.Net.Analysis.Standard;
    using Domain.BoundedContext.StoreModule;

    public class FakeLucenBCUnitOfWork : ILuceneBCUnitOfWork
    {
        private InitialMemorySet data = new InitialMemorySet();
        public LuceneDataProvider DataProvider { get; set; }
        public IDbSet<EquipmentAggregate> Equipments
        {
            get;
            set;
        }

         #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LuceneBcUnitOfWork" /> class.</summary>
        public FakeLucenBCUnitOfWork()
        {
            Directory ramDirectory = new RAMDirectory();
            var writer = new IndexWriter(ramDirectory, new StandardAnalyzer(Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED);

            this.DataProvider = new LuceneDataProvider(ramDirectory, Version.LUCENE_30, writer);
            this.DataProvider.Settings.EnableMultipleEntities = false;

            using (var session = this.DataProvider.OpenSession<EquipmentAggregate>())
            {
                    foreach(var p in data.EquipmentAggregates){
                         session.Add(KeyConstraint.Unique,p);
                    }
                
            }

            this.Equipments = new MemorySet<EquipmentAggregate>();
        }

         #endregion
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            if (this.DataProvider != null && this.Equipments != null)
            {
                using (var session = this.DataProvider.OpenSession<EquipmentAggregate>())
                {
                    foreach (var equipment in this.Equipments)
                    {
                        session.Add(KeyConstraint.Unique, equipment);
                    }

                    session.Commit();
                }
            }
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class,new()
        {
            return this.DataProvider.AsQueryable<TEntity>();
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            throw new NotImplementedException();
        }


        
    }
}
