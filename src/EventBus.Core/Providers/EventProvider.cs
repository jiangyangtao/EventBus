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
        private readonly ISubscriptionProvider _subscriptionProvider;

        public EventProvider(IRepository repository, ISubscriptionProvider subscriptionProvider) : base(repository)
        {
            _subscriptionProvider = subscriptionProvider;
        }

        public async Task<Guid> AddOrUpdateAsync(IEvent data)
        {
            var e = await Get(a => a.EventName == data.EventName).FirstOrDefaultAsync();
            if (e == null)
            {
                e = new Event(data);
                await CreateAsync(e);
                return e.Id;
            }

            e.EventName = e.EventName;
            e.EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            e.IPAddressWhiteList = e.IPAddressWhiteList;
            e.EventProtocol = e.EventProtocol;
            await UpdateAsync(e);

            return e.Id;
        }

        public async Task RemoveAsync(Guid eventId)
        {
            var e = await GetByIdAsync(eventId);
            if (e != null)
            {
                await DeleteAsync(a => a.Id == e.Id);
            }
        }

        public async Task<IEvent> GetEventAsync(Guid eventId, bool isInclude = true)
        {
            var e = await GetByIdAsync(eventId);
            if (e == null) return null;

            if (isInclude)
            {
                var subscriptions = await _subscriptionProvider.GetSubscriptionsAsync(eventId);
                if (subscriptions.NotNullAndEmpty()) e.Subscriptions = subscriptions;
            }

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

        public async Task<long> GetEventCountAsync(string eventName)
        {
            var query = Get();
            if (eventName.NotNullAndEmpty()) query.Where(a => a.EventName.Contains(eventName));

            return await query.CountAsync();
        }
    }
}
