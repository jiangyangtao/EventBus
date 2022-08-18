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
        Task<IEvent> GetEventAsync(string eventId);

        Task<IEvent[]> GetEventsAsync();

        Task<IEvent[]> GetEventsAsync(int start, int count, string enentName);
    }
}
