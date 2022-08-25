using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Services
{
    internal class SubscriptionQueueService : ISubscriptionQueueService
    {
        private readonly IBufferQueue<IEndpointSubscriptionRecord> _endpointSubscriptionRecordQueue;

        public SubscriptionQueueService(IBufferQueueService bufferQueueService)
        {
            _endpointSubscriptionRecordQueue = bufferQueueService.CreateBufferQueue<IEndpointSubscriptionRecord>("subscription",
                async subscription => await PushAsync(subscription), 10, 100);
        }

        private Task PushAsync(IEndpointSubscriptionRecord record)
        {
            // TODO 实现订阅的消费
            return Task.CompletedTask;
        }

        public Task PutAsync(IEndpointSubscriptionRecord endpointSubscriptionRecord)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(IEndpointSubscriptionRecord[] endpointSubscriptionRecords)
        {
            throw new NotImplementedException();
        }
    }
}
