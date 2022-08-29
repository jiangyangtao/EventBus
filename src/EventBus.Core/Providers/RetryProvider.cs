using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class RetryProvider : BaseRepository<RetryData>, IRetryProvider
    {
        public RetryProvider(IRepository repository) : base(repository)
        {
        }

        public Task<IRetryData[]> GetEventRetryAsync(Guid evnetId)
        {
            throw new NotImplementedException();
        }

        public Task<IRetryData> GetRetryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetRetryCountAsync(Guid subscriptionRecordId)
        {
            return await Get<EndpointSubscriptionRecord>(a => a.SubscriptionRecordId == subscriptionRecordId).CountAsync();
        }

        public Task<IRetryData[]> GetToBeExecutedRetryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
