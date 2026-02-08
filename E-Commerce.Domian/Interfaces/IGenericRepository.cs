using E_Commerce.Domian.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domian.Interfaces
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification);
        Task<int> CountAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity> AddAsync(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
