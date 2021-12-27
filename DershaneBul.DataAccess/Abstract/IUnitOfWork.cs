using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.Entities.Abstract;
using System;
using System.Threading.Tasks;

namespace DershaneBul.DataAccess.Abstract
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity, new();
        Task<int> SaveAsync();
    }
}
