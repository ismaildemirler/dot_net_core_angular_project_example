using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.Core.DataAccess.Concrete.EntityFramework;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DershaneBul.DataAccess.Concrete.EntityFramework
{
    public class UnitOfWork: IUnitOfWork
    {
        private bool disposed;
        private DershaneBulDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(DershaneBulDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO:
                //Log yapısı eklenecek
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

    }
}
