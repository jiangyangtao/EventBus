using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 事件提供者
    /// </summary>
    public interface IEventProvider
    {
        Task AddOrUpdateAsync(Event data);

        Task RemoveAsync(Event data);

        Task<Event> GetEventAsync(Guid eventId, bool isInclude = true);

        Task<Event> GetEventAsync(string eventName);

        Task<Event[]> GetEventsAsync();

        Task<Event[]> GetEventsAsync(int start, int count, string eventName);

        Task<Subscription> GetSubscriptionAsync(Guid subscriptionId);

        Task<Subscription[]> GetSubscriptionsAsync(Guid eventId);
    }
}
