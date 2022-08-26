using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IService;
using EventBus.Extensions;
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
            _endpointSubscriptionRecordQueue = bufferQueueService.CreateBufferQueue<IEndpointSubscriptionRecord>("subscription", async record => await PushAsync(record), 10, 100);
        }

        private Task PushAsync(IEndpointSubscriptionRecord record)
        {
            //if (record == null) return;

            // TODO 实现订阅的消费
            return Task.CompletedTask;
        }

        public async Task PutAsync(IEndpointSubscriptionRecord endpointSubscriptionRecord)
        {
            await _endpointSubscriptionRecordQueue.PutAsync(endpointSubscriptionRecord, default);
        }

        public async Task PutAsync(IEndpointSubscriptionRecord[] endpointSubscriptionRecords)
        {
            if (endpointSubscriptionRecords.IsNullOrEmpty()) return;

            foreach (var endpointSubscriptionRecord in endpointSubscriptionRecords)
            {
                await PutAsync(endpointSubscriptionRecord);
            }
        }
    }
}
