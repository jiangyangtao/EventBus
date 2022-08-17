﻿using EventBus.Storage.Abstractions.IRepositories;
using System.Linq.Expressions;

namespace EventBus.Storage.Mysql
{
    internal class MySqlRepository : IRepository
    {
        public Task<int> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> CreateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync<TEntity>(bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }

        public IQueryable<TSource> Get<TSource>() where TSource : IEntity
        {
            throw new NotImplementedException();
        }

        public IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate, params string[] include) where TSource : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : IEntity
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
