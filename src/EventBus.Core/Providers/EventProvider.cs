using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventProvider : BaseRepository<Entitys.Event>, IEventProvider
    {
        private readonly IApplicationProvider _applicationProvider;

        public EventProvider(IRepository repository, IApplicationProvider applicationProvider) : base(repository)
        {
            _applicationProvider = applicationProvider;
        }

        public async Task AddOrUpdateAsync(Abstractions.IModels.Event data)
        {
            var e = await Get(a => a.EventName == data.EventName).FirstOrDefaultAsync();
            if (e == null)
            {
                await CreateAsync(new Entitys.Event(data));
                return;
            }

            e.EventName = e.EventName;
            e.EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            e.IPAddressWhiteList = e.IPAddressWhiteList;
            e.EventProtocol = e.EventProtocol;
            await UpdateAsync(e);
        }

        public async Task RemoveAsync(Abstractions.IModels.Event data)
        {
            var e = await GetEventAsync(data.EventName);
            if (e != null)
            {
                await DeleteAsync(a => a.Id == e.Id);
            }
        }

        public async Task<Abstractions.IModels.Event> GetEventAsync(Guid eventId, bool isInclude = true)
        {
            var e = await GetByIdAsync(eventId);
            if (e == null) return null;

            if (isInclude)
            {
                var subscriptions = await GetSubscriptionsAsync(eventId);
                if (subscriptions.NotNullAndEmpty()) e.Subscriptions = subscriptions;
            }

            return e;
        }

        public async Task<Abstractions.IModels.Event> GetEventAsync(string eventName)
        {
            return await Get(a => a.EventName == eventName).FirstOrDefaultAsync();
        }

        public async Task<Abstractions.IModels.Event[]> GetEventsAsync()
        {
            var events = await Get().ToArrayAsync();
            if (events.IsNullOrEmpty()) return Entitys.Event.EmptyArray;

            return events;
        }

        public async Task<Abstractions.IModels.Event[]> GetEventsAsync(int start, int count, string eventName)
        {
            var query = Get();
            if (eventName.NotNullAndEmpty()) query.Where(a => a.EventName.Contains(eventName));

            var events = await query.Skip(start).Take(count).ToArrayAsync();
            if (events.IsNullOrEmpty()) return Entitys.Event.EmptyArray;

            return events;
        }

        public async Task<Abstractions.IModels.Subscription> GetSubscriptionAsync(Guid subscriptionId)
        {
            return await GetByIdAsync<Entitys.Subscription>(subscriptionId);
        }

        public async Task<Abstractions.IModels.Subscription[]> GetSubscriptionsAsync(Guid eventId)
        {
            var subscriptions = await Get<Subscription>(a => a.EvnetId == eventId).ToArrayAsync();
            if (subscriptions.IsNullOrEmpty()) return Entitys.Subscription.EmptyArray;

            var endpoints = await _applicationProvider.GetApplicationEndpointsAsync(eventId);
            if (endpoints.IsNullOrEmpty()) return Entitys.Subscription.EmptyArray;

            foreach (var subscription in subscriptions)
            {
                subscription.Event = new Entitys.Event() { Id = subscription.EvnetId };

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
