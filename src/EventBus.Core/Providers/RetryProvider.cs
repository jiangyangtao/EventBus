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

            var subscriptionRecordIds = datas.Select(a => a.SubscriptionRecordId).ToArray();
            var subscriptionRecords = await Get<SubscriptionRecord>(a => subscriptionRecordIds.Contains(a.Id)).ToArrayAsync();
            if (subscriptionRecords.NotNullAndEmpty())
            {
                await _subscriptionQueueProvider.PutAsync(subscriptionRecords);
            }

            await DeleteRangeAsync(datas);
        }

        public async Task RetryAsync(Guid retryDataId)
        {
            var data = await GetByIdAsync(retryDataId);
            var subscriptionRecord = await GetByIdAsync<SubscriptionRecord>(data.SubscriptionRecordId);
            if (subscriptionRecord != null)
            {
                subscriptionRecord.SubscriptionType = SubscriptionType.Manual;
                await _subscriptionQueueProvider.PutAsync(subscriptionRecord);
            }

            await DeleteAsync(data);
        }

        public async Task<IRetryData[]> GetEventRetryAsync(Guid evnetId)
        {
            return await Get(a => a.EventId == evnetId).ToArrayAsync();
        }

        public async Task<IRetryData> GetRetryAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<int> GetRetryCountAsync(Guid subscriptionRecordId)
        {
            return await Get<EndpointSubscriptionRecord>(a => a.SubscriptionRecordId == subscriptionRecordId).CountAsync();
        }

        public async Task<IRetryData[]> GetRetryDatasAsync(string eventName, string endpointName, int start, int count)
        {
            var query = await BuildQueryAsync(eventName, endpointName);

            var retryDatas = await query.Skip(start).Take(count).ToArrayAsync();
            if (retryDatas.IsNullOrEmpty()) return RetryData.EmptyArray;

            return retryDatas;
        }

        private async Task<IQueryable<RetryData>> BuildQueryAsync(string eventName, string endpointName)
        {
            var eventIds = Array.Empty<Guid>();
            var subscriptionRecordIds = Array.Empty<Guid>();
            if (endpointName.NotNullAndEmpty())
            {
                var events = await Get<Event>(a => a.EventName.Contains(eventName)).ToArrayAsync();
                eventIds = events.Select(a => a.Id).ToArray();
            }

            if (endpointName.NotNullAndEmpty())
            {
                var subscriptionRecords = await Get<SubscriptionRecord>(a => a.EndpointName.Contains(endpointName)).ToArrayAsync();
                subscriptionRecordIds = subscriptionRecords.Select(a => a.Id).ToArray();
            }

            var query = Get();
            if (eventIds.NotNullAndEmpty()) query.Where(a => eventIds.Contains(a.EventId));
            if (subscriptionRecordIds.NotNullAndEmpty()) query.Where(a => subscriptionRecordIds.Contains(a.SubscriptionRecordId));

            return query;
        }

        public async Task<long> GetRetryDataCountAsync(string eventName, string endpointName)
        {
            var query = await BuildQueryAsync(eventName, endpointName);

            return await query.CountAsync();
        }
    }
}
