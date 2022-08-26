using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Abstractions.IService;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventRecordManager : BaseRepository<EventRecord>, IEventRecordManager
    {
        private IEventProvider _eventProvider;
        private readonly IBufferQueue<IEndpointSubscriptionRecord> _endpointSubscriptionRecordQueue;

        public EventRecordManager(
            IRepository repository,
            IEventProvider eventProvider,
            IBufferQueueService bufferQueueService) : base(repository)
        {
            _eventProvider = eventProvider;
            _endpointSubscriptionRecordQueue = bufferQueueService.CreateBufferQueue<IEndpointSubscriptionRecord>("subscription", async record => await PushAsync(record), 10, 100);
        }

        public async Task PublishAsync(IEventRecord eventRecord)
        {
            var e = await _eventProvider.GetEventAsync(eventRecord.Event.Id);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;
            
            
            // TODO 发布事件
        }

        private Task PushAsync(IEndpointSubscriptionRecord record)
        {
            //if (record == null) return;

            // TODO 实现订阅的消费
            return Task.CompletedTask;
        }

        private async Task PutAsync(IEndpointSubscriptionRecord endpointSubscriptionRecord)
        {
            await _endpointSubscriptionRecordQueue.PutAsync(endpointSubscriptionRecord, default);
        }

        private async Task PutAsync(IEndpointSubscriptionRecord[] endpointSubscriptionRecords)
        {
            if (endpointSubscriptionRecords.IsNullOrEmpty()) return;

            foreach (var endpointSubscriptionRecord in endpointSubscriptionRecords)
            {
                await PutAsync(endpointSubscriptionRecord);
            }
        }
    }
}
