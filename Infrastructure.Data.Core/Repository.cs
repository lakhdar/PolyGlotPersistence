namespace Infrastructure.Data.Core
{
    using Domain.Core;
    using Infrastructure.CrossCutting.Core;
    using Infrastructure.Data.Core.Resources;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityBase,new()
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;
        ILogger _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        /// <param name="logger">Associated logger</param>
        public Repository(IQueryableUnitOfWork unitOfWork, ILogger logger)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");
            if (logger == (ILogger)null)
                throw new ArgumentNullException("logger");

            _UnitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Add(TEntity item)
        {
            if (item == (TEntity)null)
                throw new ArgumentNullException(typeof(TEntity).ToString(), Messages.CannotAddNullEntity);
            GetSet().Add(item); // add new item in this set
        }
        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Remove(TEntity item)
        {
            if (item == (TEntity)null)
                throw new ArgumentNullException(typeof(TEntity).ToString(), Messages.CannotRemoveNullEntity);
            //attach item if not exist
            _UnitOfWork.Attach(item);
            //set as "removed"
            GetSet().Remove(item);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Attach(TEntity item)
        {
            if (item == (TEntity)null)
                throw new ArgumentNullException(typeof(TEntity).ToString(), Messages.CannotRemoveNullEntity);
            _UnitOfWork.Attach<TEntity>(item);

        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public virtual void SetModified(TEntity item)
        {
            if (item == (TEntity)null)
                throw new ArgumentNullException(typeof(TEntity).ToString(), Messages.CannotRemoveNullEntity);
            _UnitOfWork.SetModified(item);

        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="persisted"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="current"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }
        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual TEntity GetElementById(Guid id)
        {
            if (id != Guid.Empty)
                return GetSet().Find(id);
            else
                return null;
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="cancellationToken"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public Task<TEntity> GetElementByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (id != Guid.Empty)
                return Task.FromResult(this.GetSet().Find(id));
            else
                return null;
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="cancellationToken"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="includes"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken),
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> queryable = this.GetSet();
            if (includes.Any())
                queryable = includes.Aggregate(GetSet().AsQueryable(), (current, include) => current.Include(include));
            return filter == null ? queryable.FirstOrDefaultAsync(cancellationToken) : queryable.FirstOrDefaultAsync(filter, cancellationToken);
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetAllElements()
        {
            return GetSet();
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository.GetAllElements{TEntity}"/>
        /// </summary>
        /// <param name="includes"><see cref="Domain.Core.IRepository.GetAllElements{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository.GetAllElements{TEntity}"/></returns>
        public virtual IQueryable<TEntity> GetAllElements(params Expression<Func<TEntity, object>>[] includes)
        {
            return includes
           .Aggregate(
               GetSet().AsQueryable(),
               (current, include) => current.Include(include)
           );
        }


        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Domain.Core.IRepository{TEntity}"/></typeparam>
        /// <param name="pageIndex"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.InvalidPageIndexException, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.InvalidPageCountException, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, KProperty>>)null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.OrderByExpressionCannotBeNullException);

            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)
                          .AsEnumerable();
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount)
                          .AsEnumerable();
            }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Domain.Core.IRepository{TEntity}"/></typeparam>
        /// <param name="pageIndex"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.InvalidPageIndexException, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.InvalidPageCountException, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, KProperty>>)null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.OrderByExpressionCannotBeNullException);

            if (includes == (Expression<Func<TEntity, object>>[])null)
                throw new ArgumentNullException("includes", Resources.Messages.IncludesCannotBeNullException);

            var set = GetSet();

            if (ascending)
            {
                if (includes.Any())
                {
                    return includes.Aggregate(set.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount).AsQueryable(),
                                     (current, include) => current.Include(include));
                }

                return set.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
            }
            else
            {
                if (includes.Any())
                {
                    return includes.Aggregate(set.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount).AsQueryable(),
                                            (current, include) => current.Include(include));
                }

                return set.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
            }
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository{TEntity}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Domain.Core.IRepository{TEntity}"/></typeparam>
        /// <param name="pageIndex"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="pageCount"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="orderByExpression"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <param name="ascending"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, Expression<Func<TEntity, bool>> filter, bool ascending,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (pageIndex < 0)
                throw new ArgumentException(Resources.Messages.InvalidPageIndexException, "pageIndex");

            if (pageCount <= 0)
                throw new ArgumentException(Resources.Messages.InvalidPageCountException, "pageCount");

            if (orderByExpression == (Expression<Func<TEntity, KProperty>>)null)
                throw new ArgumentNullException("orderByExpression", Resources.Messages.OrderByExpressionCannotBeNullException);

            if (includes == (Expression<Func<TEntity, object>>[])null)
                throw new ArgumentNullException("includes", Resources.Messages.IncludesCannotBeNullException);

            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", Resources.Messages.FilterCannotBeNullException);
            var set = GetSet().Where(filter);

            if (ascending)
            {
                if (includes.Any())
                {
                    return includes.Aggregate(set.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount).AsQueryable(),
                                             (current, include) => current.Include(include));
                }
                return set.OrderBy(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);

            }
            else
            {
                if (includes.Any())
                {
                    return includes.Aggregate(set.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount).AsQueryable(),
                                       (current, include) => current.Include(include));
                }
                return set.OrderByDescending(orderByExpression).Skip(pageCount * pageIndex).Take(pageCount);
            }
        }


        /// <summary>
        /// <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="Domain.Core.IRepository{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", Resources.Messages.FilterCannotBeNullException);

            return GetSet().Where(filter)
                           .AsEnumerable();
        }

        /// <summary>
        /// <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/>
        /// </summary>
        /// <param name="filter"><see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></param>
        /// <param name="includes"><see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></param>
        /// <returns><see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></returns>
        public virtual IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (filter == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("filter", Resources.Messages.FilterCannotBeNullException);

            if (includes == (Expression<Func<TEntity, object>>[])null)
                throw new ArgumentNullException("includes", Resources.Messages.IncludesCannotBeNullException);

            var query = GetSet().Where(filter);
            if (includes.Any())
            {
                return includes.Aggregate(query.AsQueryable(), (current, include) => current.Include(include));
            }
            return query;
        }

        /// <summary>
        ///  <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/>
        /// </summary>
        /// <param name="filter"> <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></param>
        /// <param name="cancellationToken"> <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></param>
        /// <param name="includes"> <see cref="Domain.Core.IRepository.GetFilteredElements{TEntity}"/></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> GetFilteredElementsAsync(Expression<Func<TEntity, bool>> filter,
            CancellationToken cancellationToken = default(CancellationToken),
            params Expression<Func<TEntity, object>>[] includes)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (filter == null)
                throw new ArgumentNullException("filter", Messages.FilterCannotBeNullException);

            IQueryable<TEntity> queryable = this.GetSet();

            if (includes.Any())
                queryable = includes.Aggregate(GetSet().AsQueryable(), (current, include) => current.Include(include));

            return Task.FromResult<IEnumerable<TEntity>>(queryable.Where<TEntity>(filter));
        }

        #endregion

        #region Private Methods

        IDbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>() as IDbSet<TEntity>;
        }
        #endregion







    }
}
