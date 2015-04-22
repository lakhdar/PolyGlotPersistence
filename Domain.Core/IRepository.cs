namespace Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Base interface for implement a "Repository Pattern", for
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <typeparam name="TEntity">Type of entity for this repository </typeparam>
    public interface IRepository<TEntity> 
        where TEntity : EntityBase
    {
        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        ///Track entity into this repository, really in UnitOfWork. 
        ///In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        void Attach(TEntity item);

        /// <summary>
        ///Set entity modified into this repository, really in UnitOfWork. 
        /// </summary>
        /// <param name="item">Item to attach</param>
        void SetModified(TEntity item);

        /// <summary>
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">entity key values, the order the are same of order in mapping.</param>
        /// <returns></returns>
        TEntity GetElementById(Guid id);

        /// <summary>
        /// Get element by entity key Async
        /// </summary>
        /// <param name="id">entity key values, the order the are same of order in mapping.</param>
        /// <returns>Task{TEntity} </returns>
        Task<TEntity> GetElementByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get all elements of type {TEntity} in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IQueryable<TEntity> GetAllElements();

         /// <summary>
        /// Get all elements of type {TEntity} in repository
        /// include specified dependencies
        /// </summary>
        /// <param name="includes">included dependencies that do match</param>
        /// <returns>IQueryable of selected elements</returns>
        IQueryable<TEntity> GetAllElements(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get first Element async 
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <param name="includes">included dependencies that do match</param>
        /// <returns>Task {TEntity}</returns>
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get all elements of type {TEntity} in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount, Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Get all elements of type {TEntity} in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <param name="includes">included navigating properties</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount, 
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending, 
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get all elements of type {TEntity} in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="filter">expression to filter a selection</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <param name="includes">included navigating properties</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPagedElements<KProperty>(int pageIndex, int pageCount, 
            Expression<Func<TEntity, KProperty>> orderByExpression,Expression<Func<TEntity, bool>> filter, bool ascending, 
            params Expression<Func<TEntity, object>>[] includes);
       
        /// <summary>
        /// Get  elements of type {TEntity} in repository satisfying a filter expression 
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Get  elements of type {TEntity} in repository satisfying a filter expression ,
        /// include specified dependecies
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="includes">included dependencies that do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFilteredElements(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Get  elements of type {TEntity} in repository satisfying a filter expression ,
        /// include specified dependecies
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <param name="includes">included dependencies that do match</param>
        /// <returns>Task IEnumerable{TEntity}</returns>
        Task<IEnumerable<TEntity>> GetFilteredElementsAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<TEntity, object>>[] includes);
    }
}
