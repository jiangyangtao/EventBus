using EventBus.Abstractions.Enums;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Core.Services;
using EventBus.Extensions;
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
            IServiceProvider serviceProvider,
            IBufferQueueService bufferQueueService,
            IHttpClientFactory httpClientFactory) : base(serviceProvider)
        {
            _subscriptionRecordQueue = bufferQueueService.CreateBufferQueue<SubscriptionRecord>("subscription", async record => await PushAsync(record), 10, 100);
            _httpClientFactory = httpClientFactory;
        }

        private async Task PushAsync(SubscriptionRecord record)
        {
            if (record == null) return;

            var endpointSubscription = await record.Subscription(_httpClientFactory, record.GetSubscriptionContent(), record.GetSubscriptionHeader());
            if (endpointSubscription != null)
            {
                var _record = await GetByIdAsync<SubscriptionRecord>(record.Id);
                if (_record != null)
                {
                    _record.SubscriptionResult = endpointSubscription.IsSuccessStatusCode;
                    await UpdateAsync(_record);
                }

                await CreateAsync(endpointSubscription);

                if (endpointSubscription.IsSuccessStatusCode == false && record.FailToRetry)
                {
                    var retryCount = await GetRetryCountAsync(record.Id);
                    var policy = record.GetRetryPolicy(retryCount);
                    if (policy.Behavior == RetryBehavior.Retry)
                    {
                        var retryData = record.GetRetryData(policy);
                        await CreateAsync(retryData);
                    }
                }

                if (endpointSubscription.IsSuccessStatusCode) // 如果成功就更新订阅成功率
                {
                    await UpdateSubscriptionSuccessRate(record.EventRecordId);
                }
            }
        }

        /// <summary>
        /// 更新订阅成功率
        /// </summary>
        /// <param name="eventRecordId"></param>
        /// <returns></returns>
        private async Task UpdateSubscriptionSuccessRate(Guid eventRecordId)
        {
            var eventRecord = await GetByIdAsync<EventRecord>(eventRecordId);
            if (eventRecord == null) return;

            var subscriptions = await Get<SubscriptionRecord>(a => a.EventRecordId == eventRecordId).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return;

            var successCount = subscriptions.Count(a => a.SubscriptionResult);
            eventRecord.SubscriptionCompletionRate = decimal.Round(successCount / subscriptions.Length, 2);  // 订阅成功率 = 成功数量 / 总数

            await UpdateAsync(eventRecord);
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
                if (record.SubscriptionResult == false) await PutAsync(record);
            }
        }
    }
}
