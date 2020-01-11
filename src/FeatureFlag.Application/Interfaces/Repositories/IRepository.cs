using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeatureFlag.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<bool> Update(int id, TEntity entity);
        Task<bool> Remove(int id);
    }
}