﻿using EventBus.Abstractions.IModels;
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
        public EventProvider(IRepository repository) : base(repository)
        {
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

            return subscriptions;
        }
    }
}
