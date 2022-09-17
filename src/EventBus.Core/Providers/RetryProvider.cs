using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class RetryProvider : BaseRepository<RetryData>, IRetryProvider
    {
        private readonly ISubscriptionQueueProvider _subscriptionQueueProvider;

        public RetryProvider(IRepository repository,
            ISubscriptionQueueProvider subscriptionQueueProvider) : base(repository)
        {
            _subscriptionQueueProvider = subscriptionQueueProvider;
        }

        public async Task RetryAsync()
        {
            var datas = await Get(a => a.RetryTime <= DateTime.Now).ToArrayAsync();
            if (datas.IsNullOrEmpty()) return;

            var eventRecordSubscriptionIds = datas.Select(a => a.EventRecordSubscriptionId).ToArray();
            var eventRecordSubscriptions = await Get<EventRecordSubscription>(a => eventRecordSubscriptionIds.Contains(a.Id)).ToArrayAsync();
            if (eventRecordSubscriptions.NotNullAndEmpty())
            {
                await _subscriptionQueueProvider.PutAsync(eventRecordSubscriptions);
            }

            await DeleteRangeAsync(datas);
        }

        public async Task RetryAsync(Guid retryDataId)
        {
            var data = await GetByIdAsync(retryDataId);
            var eventRecordSubscription = await GetByIdAsync<EventRecordSubscription>(data.EventRecordSubscriptionId);
            if (eventRecordSubscription != null && eventRecordSubscription.SubscriptionResult == false)
            {
                eventRecordSubscription.SubscriptionType = SubscriptionType.Manual;
                await _subscriptionQueueProvider.PutAsync(eventRecordSubscription);
            }

            await DeleteAsync(data);
        }

        public async Task<IRetryData[]> GetEventRetryAsync(Guid evnetId)
        {
            return await Get(a => a.EventId == evnetId).ToArrayAsync();
        }

        public async Task<IRetryData> GetRetryAsync(Guid id)
        {
            return await GetByIdAsync(a => a.Id == id);
        }

        public async Task<int> GetRetryCountAsync(Guid eventRecordSubscriptionId)
        {
            return await Get<EndpointSubscriptionRecord>(a => a.EventRecordSubscriptionId == eventRecordSubscriptionId).CountAsync();
        }

        public async Task<IRetryData[]> GetRetryDatasAsync(string eventName, string endpointName, int start, int count)
        {
            var query = await BuildQueryAsync(eventName, endpointName);

            var retryDatas = await query.Skip(start).Take(count).ToArrayAsync();
            if (retryDatas.IsNullOrEmpty()) return RetryData.EmptyArray;

            var eventIds = retryDatas.Select(a => a.EventId).Distinct().ToArray();
            var events = await Get<Event>(a => eventIds.Contains(a.Id)).ToArrayAsync();
            if (events.NotNullAndEmpty())
            {
                foreach (var retryData in retryDatas)
                {
                    retryData.Event = events.FirstOrDefault(a => a.Id == retryData.EventId);
                }
            }

            var eventRcordIds = retryDatas.Select(a => a.EventRecordId).Distinct().ToArray();
            var eventRcords = await Get<EventRecord>(a => eventRcordIds.Contains(a.Id)).ToArrayAsync();
            if (eventRcords.NotNullAndEmpty())
            {
                foreach (var retryData in retryDatas)
                {
                    retryData.EventRecord = eventRcords.FirstOrDefault(a => a.Id == retryData.EventRecordId);
                }
            }

            var eventRecordSubscriptionIds = retryDatas.Select(a => a.EventRecordSubscriptionId).Distinct().ToArray();
            var eventRecordSubscriptions = await Get<EventRecordSubscription>(a => eventRecordSubscriptionIds.Contains(a.Id)).ToArrayAsync();
            if (eventRecordSubscriptions.NotNullAndEmpty())
            {
                foreach (var retryData in retryDatas)
                {
                    retryData.EventRecordSubscription = eventRecordSubscriptions.FirstOrDefault(a => a.Id == retryData.EventRecordSubscriptionId);
                }
            }

            return retryDatas;
        }

        private async Task<IQueryable<RetryData>> BuildQueryAsync(string eventName, string endpointName)
        {
            var eventIds = Array.Empty<Guid>();
            var eventRecordSubscriptionIds = Array.Empty<Guid>();
            if (endpointName.NotNullAndEmpty())
            {
                var events = await Get<Event>(a => a.EventName.Contains(eventName)).ToArrayAsync();
                eventIds = events.Select(a => a.Id).Distinct().ToArray();
            }

            if (endpointName.NotNullAndEmpty())
            {
                var eventRecordSubscriptions = await Get<EventRecordSubscription>(a => a.EndpointName.Contains(endpointName)).ToArrayAsync();
                eventRecordSubscriptionIds = eventRecordSubscriptions.Select(a => a.Id).Distinct().ToArray();
            }

            var query = Get();
            if (eventIds.NotNullAndEmpty()) query = query.Where(a => eventIds.Contains(a.EventId));
            if (eventRecordSubscriptionIds.NotNullAndEmpty()) query = query.Where(a => eventRecordSubscriptionIds.Contains(a.EventRecordSubscriptionId));

            return query;
        }

        public async Task<long> GetRetryDataCountAsync(string eventName, string endpointName)
        {
            var query = await BuildQueryAsync(eventName, endpointName);

            return await query.CountAsync();
        }

        public async Task RemoveAsync(Guid retryDataId)
        {
            var data = await GetByIdAsync(retryDataId);

            if (data != null) await DeleteAsync(data);
        }
    }
}
