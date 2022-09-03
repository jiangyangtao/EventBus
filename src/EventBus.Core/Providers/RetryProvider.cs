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
    }
}
