using EventBus.Storage.Abstractions.IRepositories;
using System.Linq.Expressions;

namespace EventBus.Core.Base
{
    internal abstract class BaseRepository
    {
        protected readonly IRepository _repository;

        protected BaseRepository(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<TEntity> CreateAsync<TEntity>(TEntity entity, bool isCommit = true)
            where TEntity : class, IEntity
        {
            return await _repository.CreateAsync(entity, isCommit);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true)
            where TEntity : IEntity
        {
            return await _repository.AddRangeAsync(entities, isCommit);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true)
            where TEntity : IEntity
        {
            return await _repository.AddRangeAsync(entities, isCommit);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit = true)
            where TEntity : class, IEntity
        {
            return await _repository.UpdateAsync(entity, isCommit);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true)
            where TEntity : class, IEntity
        {
            return await _repository.DeleteAsync(predicate, isCommit);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity
        {
            return await _repository.DeleteAsync(entity, isCommit);
        }

        /// <summary>
        /// 删除一个对象的所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync<TEntity>(bool isCommit = true)
            where TEntity : class, IEntity
        {
            return await _repository.DeleteAsync<TEntity>(isCommit);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected Task<long> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true)
            where TEntity : IEntity
        {
            return _repository.DeleteRangeAsync(entities, isCommit);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        protected async Task<long> CommitAsync()
        {
            return await _repository.CommitAsync();
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<TSource> GetByIdAsync<TSource>(Guid id) where TSource : class, IEntity
        {
            return await _repository.GetByIdAsync<TSource>(id);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected async Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity
        {
            return await _repository.GetByIdAsync(predicate);
        }

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IQueryable<TSource> Get<TSource>()
            where TSource : class, IEntity
        {
            return _repository.Get<TSource>();
        }

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        protected IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity
        {
            return _repository.Get(predicate);
        }
    }

    internal abstract class BaseRepository<TEntity> : BaseRepository where TEntity : class, IEntity
    {
        protected BaseRepository(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<TEntity> CreateAsync(TEntity entity, bool isCommit = true) => await base.CreateAsync(entity, isCommit);


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> AddRangeAsync(List<TEntity> entities, bool isCommit = true) => await base.AddRangeAsync(entities, isCommit);


        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> AddRangeAsync(TEntity[] entities, bool isCommit = true) => await base.AddRangeAsync(entities, isCommit);


        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<TEntity> UpdateAsync(TEntity entity, bool isCommit = true) => await base.UpdateAsync(entity, isCommit);


        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) => await base.DeleteAsync(predicate, isCommit);


        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync(TEntity entity, bool isCommit = true) => await base.DeleteAsync(entity, isCommit);


        /// <summary>
        /// 删除一个对象的所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected async Task<long> DeleteAsync(bool isCommit = true) => await DeleteAsync<TEntity>(isCommit);


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        protected Task<long> DeleteRangeAsync(ICollection<TEntity> entities, bool isCommit = true) => base.DeleteRangeAsync(entities, isCommit);


        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected async Task<TEntity> GetByIdAsync(Guid id) => await GetByIdAsync<TEntity>(id);


        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate) => await base.GetByIdAsync(predicate);


        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IQueryable<TEntity> Get() => Get<TEntity>();


        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => base.Get(predicate);

    }
}
