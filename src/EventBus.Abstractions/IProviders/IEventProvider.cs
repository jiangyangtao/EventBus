using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 事件提供者
    /// </summary>
    public interface IEventProvider
    {
        Task<Guid> AddOrUpdateAsync(IEvent data);

        Task RemoveAsync(Guid eventId);

        Task<IEvent> GetEventAsync(Guid eventId, bool isInclude = true);

        Task<IEvent> GetEventAsync(string eventName);

        Task<IEvent[]> GetEventsAsync();

        Task<IEvent[]> GetEventsAsync(int start, int count, string eventName);

        Task<long> GetEventCountAsync(string eventName);
    }
}
