using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventProvider : BaseRepository<Event>, IEventProvider
    {
        private readonly IApplicationProvider _applicationProvider;

        public EventProvider(IRepository repository, IApplicationProvider applicationProvider) : base(repository)
        {
            _applicationProvider = applicationProvider;
        }

        public async Task AddOrUpdateAsync(IEvent data)
        {
            var e = await Get(a => a.EventName == data.EventName).FirstOrDefaultAsync();
            if (e == null)
            {
                await CreateAsync(new Event(data));
                return;
            }

            e.EventName = e.EventName;
            e.EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            e.IPAddressWhiteList = e.IPAddressWhiteList;
            e.EventProtocol = e.EventProtocol;
            await UpdateAsync(e);
        }

        public async Task RemoveAsync(IEvent data)
        {
            var e = await GetEventAsync(data.EventName);
            if (e != null)
            {
                await DeleteAsync(a => a.Id == e.Id);
            }
        }

        public async Task<IEvent> GetEventAsync(Guid eventId)
        {
            var e = await GetByIdAsync(eventId);
            if (e == null) return null;

            var subscriptions = await GetSubscriptionsAsync(eventId);
            if (subscriptions.NotNullAndEmpty()) e.Subscriptions = subscriptions;

            return e;
        }

        public async Task<IEvent> GetEventAsync(string eventName)
        {
            return await Get(a => a.EventName == eventName).FirstOrDefaultAsync();
        }

        public async Task<IEvent[]> GetEventsAsync()
        {
            var events = await Get().ToArrayAsync();
            if (events.IsNullOrEmpty()) return Event.EmptyArray;

            return events;
        }

        public async Task<IEvent[]> GetEventsAsync(int start, int count, string eventName)
        {
            var query = Get();
            if (eventName.NotNullAndEmpty()) query.Where(a => a.EventName.Contains(eventName));

            var events = await query.Skip(start).Take(count).ToArrayAsync();
            if (events.IsNullOrEmpty()) return Event.EmptyArray;

            return events;
        }

        public async Task<ISubscription> GetSubscriptionAsync(Guid subscriptionId)
        {
            return await GetByIdAsync<Subscription>(subscriptionId);
        }

        public async Task<ISubscription[]> GetSubscriptionsAsync(Guid eventId)
        {
            var subscriptions = await Get<Subscription>(a => a.EvnetId == eventId).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return Subscription.EmptyArray;

            var endpoints = await _applicationProvider.GetApplicationEndpointsAsync(eventId);
            if (endpoints.IsNullOrEmpty()) return Subscription.EmptyArray;

            foreach (var subscription in subscriptions)
            {
                subscription.Event = new Event() { Id = subscription.EvnetId };

                var applicationEndpoint = endpoints.FirstOrDefault(a => a.Id == subscription.ApplicationEndpointId);
                if (applicationEndpoint != null)
                {
                    subscription.ApplicationEndpoint = applicationEndpoint;
                }
            }

            return subscriptions.Where(a => a.ApplicationEndpoint != null).ToArray();
        }
    }
}
