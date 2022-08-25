using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IService
{
    public interface IBufferQueueService
    {
        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="TElement">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<TElement> CreateBufferQueue<TElement>(string name, Func<TElement, Task> handler, int concurrency = 1, int capacity = -1);

        /// <summary>
        /// 创建缓冲队列
        /// </summary>
        /// <typeparam name="TElement">元素类型</typeparam>
        /// <param name="name">缓冲队列名称</param>
        /// <param name="batchSize">批次大小</param>
        /// <param name="handler">元素处理程序</param>
        /// <param name="concurrency">处理并行度</param>
        /// <param name="capacity">最大缓冲大小</param>
        /// <returns></returns>
        IBufferQueue<TElement> CreateBufferQueue<TElement>(string name, int batchSize, Func<TElement[], Task> handler, int concurrency = 1, int capacity = -1);
    }

    /// <summary>
    /// 定义缓冲队列
    /// </summary>
    public interface IBufferQueue
    {
        /// <summary>
        /// 缓冲队列名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 缓冲队列最大容量
        /// </summary>
        int Capacity { get; }


        /// <summary>
        /// 缓冲队列当前缓存的元素数量
        /// </summary>
        int Count { get; }


        /// <summary>
        /// 关闭缓冲队列，停止接收新的数据输入
        /// </summary>
        /// <returns>返回一个 Task 对象用于等待当前缓冲队列的数据处理完毕</returns>
        Task StopAsync();

    }

    /// <summary>
    /// 定义缓冲队列
    /// </summary>
    /// <typeparam name="TElement">元素类型</typeparam>
    public interface IBufferQueue<TElement> : IBufferQueue
    {
        /// <summary>
        /// 放入一个元素到缓冲队列
        /// </summary>
        /// <param name="item">要放入的元素</param>
        /// <param name="cancellationToken">取消标识</param>
        /// <returns>是否成功推送到队列</returns>
        Task<bool> PutAsync(TElement item, CancellationToken cancellationToken);
    }
}
