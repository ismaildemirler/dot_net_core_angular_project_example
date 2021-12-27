using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DershaneBul.Core.DataAccess.Concrete.EntityFramework
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class, IBaseEntity, new()
    {
        protected DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Queryable(bool disableTracking = true)
        {
            var query = _dbSet.AsQueryable();
            if (disableTracking)
                query.AsNoTracking();
            return query;
        }

        /// <summary>
        ///Finds an entity with the given primary key values. Cant use and include.
        /// </summary>
        /// <param name="id">Primary column value. Can use multiple primary columns.</param>
        /// <returns>true / false</returns>
        public bool IsRecord(params object[] id)
        {
            bool result = false;
            var tmpObj = _dbSet.Find(id);
            if (tmpObj != null)
            {
                result = true;
                _dbContext.Entry(tmpObj).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Gets the count based on a predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await (predicate == null ? _dbSet.CountAsync() : _dbSet.CountAsync(predicate));
        }
       
        /// <summary>
        ///Finds an entity with the given primary key values. Cant use and include.
        /// </summary>
        /// <param name="id">Primary column value. Can use multiple primary columns.</param>
        /// <returns>true / false</returns>
        public async Task<bool> IsRecordAsync(params object[] id)
        {
            bool result = false;
            var tmpObj = await _dbSet.FindAsync(id);
            if (tmpObj != null)
            {
                result = true;
                _dbContext.Entry(tmpObj).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>The found entity or null.</returns>
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        /// <summary>
        /// Inserts a new entity synchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        public TEntity Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public IEnumerable<TEntity> InsertRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }

        /// <summary>
        /// Inserts a range of entities synchronously.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        public IEnumerable<TEntity> InsertRange(IEnumerable<TEntity> entities)
        {
            var insertRange = entities as TEntity[] ?? entities.ToArray();
            _dbSet.AddRange(insertRange);
            return insertRange;
        }

        public TEntity InsertOrUpdate(TEntity entity, int id = 0)
        {
            return id > 0 ? Update(entity) : Insert(entity);
        }

        public TEntity InsertOrUpdate(TEntity entity, params object[] id)
        {
            return IsRecord(id) ? Update(entity) : Insert(entity);
        }

        public TEntity Update(TEntity entity)
        {
            var tmp = _dbContext.Entry(entity);
            if (tmp == null || tmp.State == EntityState.Detached)
                _dbSet.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public void DeleteRange(List<object> ids)
        {
            var entities = new List<TEntity>();
            foreach (var id in ids)
            {
                var deleteEntity = _dbSet.Find(id);
                if (deleteEntity != null)
                    entities.Add(deleteEntity);
            }
            if (entities.Any())
                DeleteRange(entities);
        }

        public void Delete(object id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
                Delete(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        //[TransactionScopeAspect]
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (_dbContext.Entry(entity).State == EntityState.Detached)
                    {
                        _dbSet.Attach(entity);
                    }
                    _dbSet.Remove(entity);
                }
            }
        }

        /// <summary>
        /// Deletes the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void DeleteRange(params TEntity[] entities)
        {
            if (entities != null && entities.Any())
            {
                foreach (var entity in entities)
                {
                    if (_dbContext.Entry(entity).State == EntityState.Detached)
                    {
                        _dbSet.Attach(entity);
                    }
                    _dbSet.Remove(entity);
                }
            }
        }
    }
}
