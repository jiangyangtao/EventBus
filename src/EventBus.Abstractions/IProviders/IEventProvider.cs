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
        Task AddOrUpdateAsync(IEvent data);

        Task RemoveAsync(IEvent data);

        Task<IEvent> GetEventAsync(Guid eventId, bool isInclude = true);

        Task<IEvent> GetEventAsync(string eventName);

        Task<IEvent[]> GetEventsAsync();

        Task<IEvent[]> GetEventsAsync(int start, int count, string eventName);

        Task<ISubscription> GetSubscriptionAsync(Guid subscriptionId);

        Task<ISubscription[]> GetSubscriptionsAsync(Guid eventId);
    }
}
