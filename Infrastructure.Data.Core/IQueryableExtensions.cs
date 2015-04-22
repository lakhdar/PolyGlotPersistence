//===================================================================================
// 
//=================================================================================== 
// 
// 
// 
//===================================================================================
// 
// 
// 
//===================================================================================


namespace Infrastructure.Data.Core
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Helper extensions for add IDbSet methods defined only
    /// for DbSet and ObjectQuery
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Include extension method for IDbSet
        /// <example>
        /// var query = ReturnTheQuery();
        /// query = query.Include(customer=>customer.Orders);//"Orders"
        /// //or
        /// query = query.Include(customer=>customer.Orders.Select(o=>o.OrderDetails) //"Orders.OrderDetails"
        /// </example>
        /// </summary>
        /// <typeparam name="TEntity">Type of elements in IQueryable</typeparam>
        /// <typeparam name="TEntity">Type of navigated element</typeparam>
        /// <param name="queryable">Queryable object</param>
        /// <param name="path">Expression with path to include</param>
        /// <returns>Queryable object with include path information</returns>
        public static IQueryable<TEntity> Include<TEntity, TProperty>(this IDbSet<TEntity> queryable, Expression<Func<TEntity, TProperty>> path)
            where TEntity : class
        {
            var objectQuery = queryable as DbQuery<TEntity>;
            if ( objectQuery != null ) //Is DBSET
            {
                return objectQuery.Include(path);
            }
            else // probably mock
                return queryable;
        }
        /// <summary>
        /// OfType extension method for IQueryable
        /// </summary>
        /// <typeparam name="KEntity">The type to filter the elements of the sequence on. </typeparam>
        /// <param name="queryable">The queryable object</param>
        /// <returns>
        /// A new IQueryable hat contains elements from
        /// the input sequence of type TResult
        /// </returns>
        public static IQueryable<KEntity> OfType<TEntity, KEntity>(this IQueryable<TEntity> queryable)
            where TEntity : class
            where KEntity : class,TEntity
        {
            var objectQuery = queryable as DbQuery<TEntity>;

            if (objectQuery != null) //is DBSET
            {
                return objectQuery.OfType<KEntity>();
            }
            else // probably IDbSet Mock
                return queryable.OfType<KEntity>();
        }
    }
}
