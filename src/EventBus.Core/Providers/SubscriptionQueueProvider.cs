using EventBus.Abstractions.Enums;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Core.Services;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal interface ISubscriptionQueueProvider
    {
        Task PutAsync(SubscriptionRecord record);

        Task PutAsync(SubscriptionRecord[] records);
    }

    internal class SubscriptionQueueProvider : BaseRepository, ISubscriptionQueueProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBufferQueue<SubscriptionRecord> _subscriptionRecordQueue;

        public SubscriptionQueueProvider(
            IRepository repository,
               IBufferQueueService bufferQueueService,
               IHttpClientFactory httpClientFactory) : base(repository)
        {
            _subscriptionRecordQueue = bufferQueueService.CreateBufferQueue<SubscriptionRecord>("subscription", async record => await PushAsync(record), 10, 100);
            _httpClientFactory = httpClientFactory;
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
                    var retryCount = await GetRetryCountAsync(record.EventRecordId);
                    var policy = record.GetRetryPolicy(retryCount);
                    if (policy.Behavior == RetryBehavior.Retry)
                    {
                        var retryData = record.GetRetryData(policy);
                        await CreateAsync(retryData);
                    }
                }
            }
        }

        private async Task<int> GetRetryCountAsync(Guid subscriptionRecordId)
        {
            return await Get<EndpointSubscriptionRecord>(a => a.SubscriptionRecordId == subscriptionRecordId).CountAsync();
        }

        public async Task PutAsync(SubscriptionRecord record)
        {
            await _subscriptionRecordQueue.PutAsync(record, default);
        }

        public async Task PutAsync(SubscriptionRecord[] records)
        {
            if (records.IsNullOrEmpty()) return;

            foreach (var record in records)
            {
                await PutAsync(record);
            }
        }
    }
}
