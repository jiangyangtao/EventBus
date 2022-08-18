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

        private IMongoCollection<TDocument> SetCollection<TDocument>() =>
            _db.GetCollection<TDocument>(typeof(TDocument).Name);

        public async Task<long> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            return await AddRangeAsync(entities.ToArray(), isCommit);
        }

        public async Task<long> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : IEntity
        {
            if (entities.IsNullOrEmpty()) return 0;

            foreach (var item in entities)
            {
                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }

            await SetCollection<TEntity>().InsertManyAsync(entities);
            if (isCommit) await CommitAsync();

            return entities.Length;
        }

        public async Task<long> CommitAsync()
        {
            await _sessionHandle.CommitTransactionAsync();
            return 1;
        }

        public async Task<long> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            if (entity == null) return 0;

            var r = await SetCollection<TEntity>().DeleteOneAsync(a => a.Id == entity.Id);
            if (isCommit) await CommitAsync();

            return r.DeletedCount;
        }

        public async Task<long> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            if (entities.IsNullOrEmpty()) return 0;

            var ids = entities.Select(a => a.Id).ToArray();
            var r = await SetCollection<TEntity>().DeleteManyAsync(a => ids.Contains(a.Id));

            if (isCommit) await CommitAsync();

            return r.DeletedCount;
        }

        public async Task<TEntity> CreateAsync<TEntity>(TEntity entity, bool isCommit) where TEntity : class, IEntity
        {
            if (entity == null) return null;

            await SetCollection<TEntity>().InsertOneAsync(entity);
            if (isCommit) await CommitAsync();

            return entity;
        }

        public async Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit)
            where TEntity : class, IEntity
        {
            var r = await SetCollection<TEntity>().DeleteManyAsync(predicate);
            if (isCommit) await CommitAsync();

            return r.DeletedCount;
        }

        public async Task<long> DeleteAsync<TEntity>(bool isCommit) where TEntity : class, IEntity
        {
            var r = await SetCollection<TEntity>().DeleteManyAsync(a => a.CreateTime > new DateTime(0, 0, 0, 0, 0, 0, 0, DateTimeKind.Utc));
            return r.DeletedCount;
        }

        public IQueryable<TSource> Get<TSource>() where TSource : class, IEntity
        {
            return SetCollection<TSource>().AsQueryable();
        }

        public IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity
        {
            return SetCollection<TSource>().AsQueryable().Where(predicate);
        }

        public async Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity
        {
            return await SetCollection<TSource>().Find(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit) where TEntity : class, IEntity
        {
            if (entity == null) return null;

            entity.UpdateTime = DateTime.Now;
            var filter = Builders<TEntity>.Filter.Where(a => a.Id == entity.Id);

            // TODO 处理要利用反射找出有更新的字段，再一对一的生成
            var updateColumns = Builders<TEntity>.Update.Set(a => a.UpdateTime, DateTime.Now);
            await SetCollection<TEntity>().UpdateOneAsync(filter, updateColumns);

            if (isCommit) await CommitAsync();

            return entity;
        }
    }
}
