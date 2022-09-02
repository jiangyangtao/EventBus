using EventBus.Abstractions.Models;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventRecordProvider
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventRecord"></param>
        /// <returns></returns>
        Task PublishAsync(EventRecord eventRecord);

        Task<EventRecord> GetEventRecordAsync(Guid eventRecordId);

        Task<EventRecord[]> GetEventRecordsAsync(Guid eventId);

        Task<EventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end);
    }
}
