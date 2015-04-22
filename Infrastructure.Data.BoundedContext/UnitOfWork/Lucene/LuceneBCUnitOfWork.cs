// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LuceneBCUnitOfWork.cs" company="">
//   
// </copyright>
// <summary>
//   The Lucene unit of work.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Infrastructure.Data.BoundedContext.UnitOfWork.Lucene
{
    using Domain.BoundedContext.StoreModule;
    using global::Lucene.Net.Analysis.Standard;
    using global::Lucene.Net.Index;
    using global::Lucene.Net.Linq;
    using global::Lucene.Net.Store;
    using Infrastructure.Data.Core;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Version = global::Lucene.Net.Util.Version;

    /// <summary>The Lucene unit of work.</summary>
    public class LuceneBcUnitOfWork : ILuceneBCUnitOfWork
    {
        private InitialMemorySet data = new InitialMemorySet();

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="LuceneBcUnitOfWork" /> class.</summary>
        public LuceneBcUnitOfWork()
        {
            Directory ramDirectory = FSDirectory.Open(@"C:\lucene\index");
            var writer = new IndexWriter(ramDirectory, new StandardAnalyzer(Version.LUCENE_30), IndexWriter.MaxFieldLength.UNLIMITED);

            this.DataProvider = new LuceneDataProvider(ramDirectory, Version.LUCENE_30, writer);
            this.DataProvider.Settings.EnableMultipleEntities = false;

            using (var session = this.DataProvider.OpenSession<EquipmentAggregate>())
            {
                //session.DeleteAll();
                foreach (var p in data.EquipmentAggregates)
                {
                    session.Add(KeyConstraint.Unique, p);
                }
            }

            this.Equipments = new MemorySet<EquipmentAggregate>();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets the equipments.</summary>
        public IDbSet<EquipmentAggregate> Equipments { get; set; }


        #endregion

        #region Properties

        /// <summary>Gets or sets the data provider.</summary>
        public LuceneDataProvider DataProvider { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add.</summary>
        /// <param name="equipment">The equipment.</param>
        public void Add(EquipmentAggregate equipment)
        {
            if (!this.Equipments.Contains(equipment))
            {
                this.Equipments.Add(equipment);
            }
        }

        /// <summary>The apply current values.</summary>
        /// <param name="original">The original.</param>
        /// <param name="current">The current.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            throw new NotImplementedException();
        }

        /// <summary>The attach.</summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        /// <summary>The commit.</summary>
        /// <exception cref="NotImplementedException"></exception>
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

        /// <summary>The commit and refresh changes.</summary>
        /// <exception cref="NotImplementedException"></exception>
        public void CommitAndRefreshChanges()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>The commit and refresh changes async.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>The commit async.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        /// <summary>The create set.</summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The <see cref="IQueryable"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class,new()
        {
            return this.DataProvider.AsQueryable<TEntity>();
        }

        /// <summary>The dispose.</summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Dispose()
        {
            if (this.DataProvider != null)
            {
                this.DataProvider.Dispose();
            }
        }

        /// <summary>The execute command.</summary>
        /// <param name="sqlCommand">The sql command.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The <see cref="int"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>The execute query.</summary>
        /// <param name="sqlQuery">The sql query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The <see cref="IEnumerable"/>.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>The rollback changes.</summary>
        /// <exception cref="NotImplementedException"></exception>
        public void RollbackChanges()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>The set modified.</summary>
        /// <param name="item">The item.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <exception cref="NotImplementedException"></exception>
        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        #endregion

       
    }
}