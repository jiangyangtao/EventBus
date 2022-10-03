using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventBus.Storage.Core
{
    internal class Repository : IRepository
    {
        private readonly EventBusDBContext _dbContext;

        public Repository(EventBusDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : class, IEntity
        {
            return await AddRangeAsync(entities.ToArray(), isCommit);
        }

        public async Task<long> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : class, IEntity
        {
            if (entities.IsNullOrEmpty()) return 0;

            foreach (var item in entities)
            {
                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }

            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : class, IEntity
        {
            if (entity == null) return null;

            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            await _dbContext.AddAsync(entity);

            if (isCommit) await CommitAsync();

            return entity;
        }

        public async Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) where TEntity : class, IEntity
        {
            var entities = await Get(predicate).ToArrayAsync();
            if (entities.IsNullOrEmpty()) return 0;

            _dbContext.RemoveRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            if (entity == null) return 0;

            _dbContext.Remove(entity);
            if (isCommit) await CommitAsync();

            return 1;
        }

        public async Task<long> DeleteAsync<TEntity>(bool isCommit = true) where TEntity : class, IEntity
        {
            var entities = await Get<TEntity>().ToArrayAsync();
            if (entities.IsNullOrEmpty()) return 0;

            _dbContext.RemoveRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : class, IEntity
        {
            if (entities.IsNullOrEmpty()) return 0;

            _dbContext.Set<TEntity>().RemoveRange(entities);
            if (isCommit) await CommitAsync();

            return entities.Count;
        }

        public IQueryable<TSource> Get<TSource>() where TSource : class, IEntity
        {
            return _dbContext.Set<TSource>();
        }

        public IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity
        {
            return _dbContext.Set<TSource>().Where(predicate);
        }

        public async Task<TSource> GetByIdAsync<TSource>(Guid id) where TSource : class, IEntity
        {
            var r = await _dbContext.Set<TSource>().FirstOrDefaultAsync(a => a.Id == id);
            if (r == null) return null;

            return r;
        }

        public async Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity
        {
            return await _dbContext.Set<TSource>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : class, IEntity
        {
            if (entity == null) return null;

            entity.UpdateTime = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);

            if (isCommit) await CommitAsync();

            return entity;
        }
    }
}
