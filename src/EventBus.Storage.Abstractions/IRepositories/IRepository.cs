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
        /// <summary>
        /// 创建一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> Create<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> AddRange<TEntity>(List<TEntity> entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> AddRange<TEntity>(TEntity[] entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<TEntity> Update<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> Delete<TEntity>(Expression<Func<TEntity, bool>> predicate, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> Delete<TEntity>(TEntity entity, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 删除一个对象的所有数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> Delete<TEntity>(bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <param name="isCommit"></param>
        /// <returns></returns>
        Task<int> DeleteRange<TEntity>(ICollection<TEntity> entities, bool isCommit = true) where TEntity : IEntity;

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        Task<int> Commit();

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TSource> GetById<TSource>(Expression<Func<TSource, bool>> predicate) where TSource : IEntity;

        /// <summary>
        /// 获取一个 IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IQueryable<T> Get<T>() where T : class;

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
