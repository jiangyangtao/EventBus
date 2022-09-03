using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventRecordProvider
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventRecord"></param>
        /// <returns></returns>
        Task PublishAsync(Guid eventId);

        Task<IEventRecord> GetEventRecordAsync(Guid eventRecordId);

        Task<IEventRecord[]> GetEventRecordsAsync(Guid eventId);

        Task<IEventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end);
    }
}
