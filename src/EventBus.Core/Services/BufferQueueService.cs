using Castle.Core.Logging;
using EventBus.Abstractions.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks.Dataflow;

namespace EventBus.Core.Services
{
    internal class BufferQueueService : IHostedService, IBufferQueueService
    {
        private readonly ILogger<IBufferQueueService> _logger;
        private readonly ICollection<IBufferQueue> _queueCollection = new List<IBufferQueue>();
        private volatile bool _stopping = false;


        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _stopping = true;
            return Task.WhenAll(_queueCollection.Select(item => item.StopAsync()));
        }

        public BufferQueueService(ILogger<IBufferQueueService> logger)
        {
            _logger = logger;
        }


        public IBufferQueue<TElement> CreateBufferQueue<TElement>(string name, Func<TElement, Task> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");

            var queue = new BufferQueue<TElement>(name, CreateHandler(_logger, handler), concurrency, bound);
            _queueCollection.Add(queue);

            return queue;
        }


        public IBufferQueue<T> CreateBufferQueue<T>(string name, int batchSize, Func<T[], Task> handler, int concurrency = 1, int bound = -1)
        {
            if (_stopping)
                throw new InvalidOperationException("application is stopping.");
            var queue = new BatchBufferQueue<T>(name, batchSize, CreateHandler(_logger, handler), concurrency, bound);

            _queueCollection.Add(queue);

            return queue;
        }


        protected Func<T, Task> CreateHandler<T>(Microsoft.Extensions.Logging.ILogger logger, Func<T, Task> handler)
        {
            return async item =>
            {
                await Task.Run(async () =>
                {
                    try
                    {
                        await handler(item);
                    }
                    catch (Exception e)
                    {
                        logger.LogError($"handle message failed.\n{e}");
                    }
                }
                );
            };

        }


        private abstract class BufferQueueBase<T> : IBufferQueue<T>
        {
            public BufferQueueBase(string name, int maxCuncurrency, int capacity = -1)
            {
                Name = name;
                MaxCuncurrency = maxCuncurrency;
                Capacity = capacity;
            }

            public int MaxCuncurrency { get; }

            public string Name { get; }

            public int Capacity { get; }

            public abstract int Count { get; }

            public abstract Task<bool> PutAsync(T item, CancellationToken cancellationToken);

            public abstract Task StopAsync();
        }


        /// <summary>
        /// 实现一个缓冲队列
        /// </summary>
        /// <typeparam name="T">队列对象类型</typeparam>
        private sealed class BufferQueue<T> : BufferQueueBase<T>
        {


            private readonly ActionBlock<T> _block;

            public override int Count => _block.InputCount;


            /// <summary>
            /// 创建 BufferQueue 实例
            /// </summary>
            /// <param name="provider">创建这个缓冲队列的缓冲队列提供程序</param>
            /// <param name="name"></param>
            /// <param name="handler">队列处理程序</param>
            /// <param name="maxConcurrency">最大并发</param>
            /// <param name="capacity">队列最大容量，默认为无限制</param>
            internal BufferQueue(string name, Func<T, Task> handler, int maxConcurrency, int capacity) : base(name, maxConcurrency, capacity)
            {
                _block = new ActionBlock<T>(handler, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxConcurrency, BoundedCapacity = capacity });
            }



            /// <summary>
            /// 往缓冲队列中放入一个对象
            /// </summary>
            /// <param name="item">要放入的对象</param>
            /// <param name="cancellationToken">取消标识</param>
            /// <returns>是否成功推送到缓冲队列</returns>
            public override Task<bool> PutAsync(T item, CancellationToken cancellationToken)
            {
                return Task.Run(() =>
                {
                    return _block.SendAsync(item, cancellationToken);
                });
            }


            /// <summary>
            /// 在停止之前进行清理操作，等待所有任务处理完成
            /// </summary>
            /// <returns></returns>
            public override Task StopAsync()
            {
                _block.Complete();
                return _block.Completion;
            }
        }

        private sealed class BatchBufferQueue<T> : BufferQueueBase<T>
        {
            private readonly BatchBlock<T> batchBlock;
            private readonly ActionBlock<T[]> actionBlock;

            public BatchBufferQueue(string name, int batchSize, Func<T[], Task> handler, int maxConcurrency, int capacity = -1)
              : base(name, maxConcurrency, capacity)
            {
                batchBlock = new BatchBlock<T>(batchSize, new GroupingDataflowBlockOptions { BoundedCapacity = capacity });
                actionBlock = new ActionBlock<T[]>(handler, new ExecutionDataflowBlockOptions { BoundedCapacity = maxConcurrency, MaxDegreeOfParallelism = maxConcurrency });

                batchBlock.LinkTo(actionBlock);
            }

            public override int Count => batchBlock.OutputCount;

            public override Task<bool> PutAsync(T item, CancellationToken cancellationToken)
            {
                return batchBlock.SendAsync(item, cancellationToken);
            }

            public override async Task StopAsync()
            {
                batchBlock.Complete();
                await batchBlock.Completion;
            }
        }
    }
}
