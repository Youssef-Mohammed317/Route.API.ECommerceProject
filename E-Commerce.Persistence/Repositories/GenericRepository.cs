using E_Commerce.Domian.Entites;
using E_Commerce.Domian.Interfaces;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification)
        {
            var query = SpecificationEvaluater.CreateCountQuery(_dbContext.Set<TEntity>(), specification);

            return await query.CountAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
            var query = SpecificationEvaluater.CreateQuery(_dbContext.Set<TEntity>(), specification);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return (await _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => e.Id!.Equals(id)))!;
        }
        public async Task<TEntity> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            var query = SpecificationEvaluater.CreateQuery(_dbContext.Set<TEntity>(), specification);

            return (await query.FirstOrDefaultAsync())!;
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}
