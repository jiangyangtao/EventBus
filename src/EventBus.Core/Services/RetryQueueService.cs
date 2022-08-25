using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Services
{
    internal class RetryQueueService : IRetryQueueService
    {
        private readonly IBufferQueue<IRetryData> _retryDataQueue;

        public RetryQueueService(IBufferQueueService bufferQueueService)
        {
            _retryDataQueue = bufferQueueService.CreateBufferQueue<IRetryData>("Retry", 1,
                async dictionary => await PushAsync(dictionary), 10, 100);
        }

        public Task PutAsync(IRetryData retryData)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(IRetryData[] retryDatas)
        {
            throw new NotImplementedException();
        }

        private Task PushAsync(IRetryData[] datas)
        {
            // TODO 实现重试
            return Task.CompletedTask;
        }
    }
}
