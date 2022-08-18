using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace EventBus.Storage.MongoDB
{
    internal class MongoDBRepository : IRepository
    {
        private readonly IMongoDatabase _db;
        private readonly IClientSessionHandle _sessionHandle;

        public MongoDBRepository(MongoClient mongoClient)
        {
            _sessionHandle = mongoClient.StartSession();
            _db = mongoClient.GetDatabase("");
        }

        private IMongoCollection<TDocument> GetMongoCollection<TDocument>() =>
            _db.GetCollection<TDocument>(typeof(TDocument).Name);

        public async Task<int> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            return await AddRangeAsync(entities.ToArray(), isCommit);
        }

        public async Task<int> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : IEntity
        {
            if (entities.IsNullOrEmpty()) return 0;

            foreach (var item in entities)
            {
                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }

            await GetMongoCollection<TEntity>().InsertManyAsync(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<int> CommitAsync()
        {
            await _sessionHandle.CommitTransactionAsync();
            return 1;
        }

        public Task<int> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> CreateAsync<TEntity>(TEntity entity, bool isCommit) where TEntity : class, IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit)
            where TEntity : class, IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(bool isCommit) where TEntity : class, IEntity
        {
            throw new NotImplementedException();
        }

        public IQueryable<TSource> Get<TSource>() where TSource : class, IEntity
        {
            return GetMongoCollection<TSource>().AsQueryable();
        }

        public IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity
        {
            return GetMongoCollection<TSource>().AsQueryable().Where(predicate);
        }

        public Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit) where TEntity : class, IEntity
        {
            throw new NotImplementedException();
        }
    }
}
