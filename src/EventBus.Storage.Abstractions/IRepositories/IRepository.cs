using EventBus.Storage.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Abstractions.IRepositories
{
    public interface IRepository
    {
        public StorageType StorageType { get; }

        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> DeleteAsync<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 删除一个对象的所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> DeleteAsync<TEntity>(bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> DeleteRangeAsync<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TSource> GetByIdAsync<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : IEntity;

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<TSource> Get<TSource>() where TSource : IEntity;

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        IQueryable<TSource> Get<TSource>(Expression<Func<TSource, bool>> predicate, params string[] include) where TSource : IEntity;
    }
}
