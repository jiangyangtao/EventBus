using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class RetryManager : BaseRepository<RetryData>, IRetryManager
    {
        private readonly ISubscriptionQueueProvider _subscriptionQueueProvider;

        public RetryManager(IRepository repository,
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
            if (subscriptionRecordIds.NotNullAndEmpty()) await _subscriptionQueueProvider.PutAsync(subscriptionRecords);
        }

        public async Task RetryAsync(Guid retryDataId)
        {
            var data = await GetByIdAsync(retryDataId);
            var subscriptionRecord = await GetByIdAsync<SubscriptionRecord>(data.SubscriptionRecordId);
            if (subscriptionRecord != null) await _subscriptionQueueProvider.PutAsync(subscriptionRecord);
        }
    }
}
