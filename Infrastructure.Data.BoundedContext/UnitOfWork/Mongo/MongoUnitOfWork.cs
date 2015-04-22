namespace Infrastructure.Data.BoundedContext.UnitOfWork.Mongo
{
    using Domain.BoundedContext.ERPModule;
    using Infrastructure.Data.BoundedContext.Resources;
    using Infrastructure.Data.Core;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        #region Fields
        string _dbHostName;
        string _dbName;
        MongoDatabase _database;

        private InitialMemorySet data = new InitialMemorySet();
       
        #endregion


        #region properties
        public string DbName
        {
            get {
                if (string.IsNullOrEmpty(this._dbName))
                {
                    this._dbName = "polyGlotDemo";
                }
                return _dbName;
            }
            set { _dbName = value; }
        }
        public string DbHostName
        {
            get {
                if (string.IsNullOrEmpty(this._dbHostName))
                {
                    this._dbHostName = "127.0.0.1";
                }
                return _dbHostName;
            }
            set { _dbHostName = value; }
        }

        public MongoDatabase Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public IDbSet<DepartmentAggregate> Departments { get; set; }

        #endregion

        #region Ctor
        public MongoUnitOfWork()
        {
            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("MongoUnitOfWorkPack", pack, (t) => true);
            string connectionString = "mongodb://" + this.DbHostName;
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.WriteConcern.Journal = true;
            var mongoClient = new MongoClient(settings);
            var mongoServer = mongoClient.GetServer();

            if (!mongoServer.DatabaseExists(this.DbName))
            {
                throw new MongoException(string.Format(CultureInfo.CurrentCulture, Messages.DatabaseDoesNotExist, this.DbName));
            }

            this.Database = mongoServer.GetDatabase(this.DbName);
            var coll = this.Database.GetCollection<DepartmentAggregate>("DepartmentAggregate");
            //coll.RemoveAll();
            foreach (var dep in data.DepartmentAggregates)
            {
                if (!coll.AsQueryable().Any(x => x.Id == dep.Id))
                {
                    coll.Insert<DepartmentAggregate>(dep);
                }
            }
            this.Departments = new MemorySet<DepartmentAggregate>();
        }
        #endregion


        #region IQueryableUnitOfWork
        public IQueryable<TEntity> CreateSet<TEntity>() where TEntity : class, new()
        {
            string name = typeof(TEntity).Name;
            return  this.Database.GetCollection<TEntity>(name).AsQueryable();
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        #endregion 

        #region IUnitOfWork
        public void Commit()
        {
           var coll= this.Database.GetCollection<DepartmentAggregate>("DepartmentAggregate");
            foreach (var dep in this.Departments)
            {
                coll.Insert<DepartmentAggregate>(dep);
            }
        }

        public void CommitAndRefreshChanges()
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAndRefreshChangesAsync(CancellationToken cancellationToken =  default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
