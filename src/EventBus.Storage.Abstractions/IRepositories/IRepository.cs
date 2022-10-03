using System.Linq.Expressions;

namespace EventBus.Storage.Abstractions.IRepositories
{
    public interface IRepository
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 删除一个对象的所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<long> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : class, IEntity;

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task<long> CommitAsync();

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : class, IEntity;

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TSource> GetByIdAsync<TSource>(Guid id) where TSource : class, IEntity;

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<TSource> Get<TSource>() where TSource : class, IEntity;

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate)
            where TSource : class, IEntity;
    }
}
