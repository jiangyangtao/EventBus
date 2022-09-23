using EventBus.Abstractions;
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
        Task PutAsync(EventRecordSubscription record);

        Task PutAsync(EventRecordSubscription[] records);
    }

    internal class SubscriptionQueueProvider : BaseRepository, ISubscriptionQueueProvider
    {
        private readonly ISubscriptionFactory _subscriptionFactory;
        private readonly IBufferQueue<EventRecordSubscription> _eventRecordSubscriptionIdQueue;

        public SubscriptionQueueProvider(
            IServiceProvider serviceProvider,
            IBufferQueueService bufferQueueService,
            ISubscriptionFactory subscriptionFactory) : base(serviceProvider)
        {
            _eventRecordSubscriptionIdQueue = bufferQueueService.CreateBufferQueue<EventRecordSubscription>("subscription", async record => await PushAsync(record), 10, 100);
            _subscriptionFactory = subscriptionFactory;
        }

        private async Task PushAsync(EventRecordSubscription record)
        {
            if (record == null) return;

            var subscriptionProvider = _subscriptionFactory.CreateSubscriptionProvider(record);
            var endpointSubscription = await subscriptionProvider.SubscriptionAsync();
            if (endpointSubscription != null)
            {
                var _record = await GetByIdAsync<EventRecordSubscription>(record.Id);
                if (_record != null)
                {
                    _record.SubscriptionResult = endpointSubscription.IsSuccessStatusCode;
                    await UpdateAsync(_record);
                }

                await CreateAsync(new EndpointSubscriptionRecord(endpointSubscription));

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

            var subscriptions = await Get<EventRecordSubscription>(a => a.EventRecordId == eventRecordId).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return;

            var successCount = subscriptions.Count(a => a.SubscriptionResult);
            eventRecord.SubscriptionCompletionRate = decimal.Round(successCount / subscriptions.Length, 2);  // 订阅成功率 = 成功数量 / 总数

            await UpdateAsync(eventRecord);
        }

        private async Task<int> GetRetryCountAsync(Guid eventRecordSubscriptionId)
        {
            return await Get<EndpointSubscriptionRecord>(a => a.EventRecordSubscriptionId == eventRecordSubscriptionId).CountAsync();
        }

        public async Task PutAsync(EventRecordSubscription subscription)
        {
            await _eventRecordSubscriptionIdQueue.PutAsync(subscription, default);
        }

        public async Task PutAsync(EventRecordSubscription[] subscriptions)
        {
            if (subscriptions.IsNullOrEmpty()) return;

            foreach (var record in subscriptions)
            {
                if (record.SubscriptionResult == false) await PutAsync(record);
            }
        }
    }
}
