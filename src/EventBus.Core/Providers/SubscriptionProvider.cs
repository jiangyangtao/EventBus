using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace EventBus.Core.Providers
{
    internal class SubscriptionProvider : BaseRepository<Subscription>, ISubscriptionProvider
    {
        private readonly IApplicationProvider _applicationProvider;

        public SubscriptionProvider(IRepository repository, IApplicationProvider applicationProvider) : base(repository)
        {
            _applicationProvider = applicationProvider;
        }

        public async Task<Guid> AddAsync(Guid eventId, Guid endpointId)
        {
            var endpoint = await _applicationProvider.GetApplicationEndpointAsync(endpointId);
            if (endpoint == null) return Guid.Empty;

            var subscription = new Subscription(eventId, endpoint);
            await CreateAsync(subscription);

            return subscription.Id;
        }

        public async Task<ISubscription> GetSubscriptionAsync(Guid subscriptionId)
        {
            return await GetByIdAsync<Subscription>(subscriptionId);
        }

        public async Task<long> GetSubscriptionCountAsync(Guid? eventId, string endpointName)
        {
            var query = BuildQueryable(eventId, endpointName);
            return await query.CountAsync();
        }

        private IQueryable<Subscription> BuildQueryable(Guid? eventId, string endpointName)
        {
            var query = Get();
            if (eventId.HasValue) query.Where(a => a.EventId == eventId);
            if (endpointName.NotNullAndEmpty()) query.Where(a => a.EndpointName.Contains(endpointName));

            return query;
        }

        public async Task<ISubscription[]> GetSubscriptionsAsync(Guid? eventId, string endpointName, int start, int count)
        {
            var query = BuildQueryable(eventId, endpointName);

            var subscriptions = await query.Skip(start).Take(count).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return Subscription.EmptyArray;

            var eventIds = subscriptions.Select(a => a.EventId).ToArray();
            if (eventIds.NotNullAndEmpty())
            {
                var events = await Get<Event>(a => eventIds.Contains(a.Id)).ToArrayAsync();
                foreach (var subscription in subscriptions)
                {
                    subscription.Event = events.FirstOrDefault(a => a.Id == subscription.EventId);
                }
            }

            return subscriptions;
        }

        public async Task<ISubscription[]> GetSubscriptionsAsync(Guid eventId)
        {
            var subscriptions = await Get<Subscription>(a => a.EventId == eventId).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return Subscription.EmptyArray;

            return subscriptions;
        }

        public async Task RemoveAsync(Guid subscriptionId)
        {
            await DeleteAsync(a => a.Id == subscriptionId);
        }
    }
}
