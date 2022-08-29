using EventBus.Abstractions.Enums;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventRecordManager : BaseRepository<EventRecord>, IEventRecordManager
    {
        private readonly IEventProvider _eventProvider;
        private readonly IApplicationProvider _applicationProvider;
        private readonly IRetryProvider _retryProvider;
        private readonly IBufferQueue<SubscriptionRecord> _subscriptionRecordQueue;
        private readonly IHttpClientFactory _httpClientFactory;

        public EventRecordManager(
            IRepository repository,
            IEventProvider eventProvider,
            IBufferQueueService bufferQueueService,
            IApplicationProvider applicationProvider,
            IHttpClientFactory httpClientFactory,
            IRetryProvider retryProvider) : base(repository)
        {
            _eventProvider = eventProvider;
            _subscriptionRecordQueue = bufferQueueService.CreateBufferQueue<SubscriptionRecord>("subscription", async record => await PushAsync(record), 10, 100);
            _applicationProvider = applicationProvider;
            _httpClientFactory = httpClientFactory;
            _retryProvider = retryProvider;
        }

        public async Task PublishAsync(IEventRecord eventRecord)
        {
            var e = await _eventProvider.GetEventAsync(eventRecord.Event.Id);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;

            await CreateAsync(new EventRecord(eventRecord));

            if (e.Subscriptions.NotNullAndEmpty())
            {
                var records = e.Subscriptions.Where(subscription => subscription.ApplicationEndpoint != null)
                    .Select(subscription => new SubscriptionRecord(eventRecord, subscription.ApplicationEndpoint)).ToArray();

                if (records.NotNullAndEmpty())
                {
                    await AddRangeAsync(records);
                    await PutAsync(records);
                }
            }
        }


        private async Task PushAsync(SubscriptionRecord record)
        {
            if (record == null) return;

            var endpointSubscription = await record.Subscription(_httpClientFactory, record.GetSubscriptionContent());
            if (endpointSubscription != null)
            {
                record.SubscriptionResult = endpointSubscription.IsSuccessStatusCode;
                await UpdateAsync(record, false);
                await CreateAsync(endpointSubscription);

                if (endpointSubscription.IsSuccessStatusCode == false)
                {
                    var retryCount = await _retryProvider.GetRetryCountAsync(record.EventRecordId);
                    var policy = record.GetRetryPolicy(retryCount);
                    if (policy.Behavior == RetryBehavior.Retry)
                    {
                        var retryData = record.GetRetryData(policy);
                        await CreateAsync(retryData);
                    }
                }
            }
        }

        private async Task PutAsync(SubscriptionRecord record)
        {
            await _subscriptionRecordQueue.PutAsync(record, default);
        }

        private async Task PutAsync(SubscriptionRecord[] records)
        {
            if (records.IsNullOrEmpty()) return;

            foreach (var record in records)
            {
                await PutAsync(record);
            }
        }
    }
}
