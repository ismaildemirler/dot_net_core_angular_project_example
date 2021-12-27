using DershaneBul.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DershaneBul.Core.DataAccess.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        IQueryable<TEntity> Queryable(bool disableTracking = true);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        bool IsRecord(params object[] id);

        /// <summary>
        ///Finds an entity with the given primary key values. Cant use and include.
        /// </summary>
        /// <param name="id">Primary column value. Can use multiple primary columns.</param>
        /// <returns>true / false</returns>
        Task<bool> IsRecordAsync(params object[] id);

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        IEnumerable<TEntity> InsertRange(params TEntity[] entities);

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Inserts or Update a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        TEntity InsertOrUpdate(TEntity entity, int id = 0);

        TEntity InsertOrUpdate(TEntity entity, params object[] id);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes the entity by the specified primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        void DeleteRange(List<object> ids);

        void Delete(object id);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(params TEntity[] entities);

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
