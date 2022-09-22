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

        /// <summary>
        /// 发布事件，gRPC 方式
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        Task PublishAsync(Guid eventId, IEventRecord record);

        Task SubscriptionAsync(Guid subscriptionId);

        Task<IEventRecord> GetEventRecordAsync(Guid eventRecordId);

        Task<IEventRecord[]> GetEventRecordsAsync(Guid eventId);

        Task<IEventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end);

        Task<long> GetEventRecordCountAsync(DateTime? begin, DateTime? end);

        Task<IEventRecordSubscription> GetEventRecordSubscriptionAsync(Guid eventRecordSubscriptionId);

        Task<IEventRecordSubscription[]> GetEventRecordSubscriptionsAsync(Guid eventRecordId);

        Task<IEndpointSubscriptionRecord[]> GetEndpointSubscriptionRecordsAsync(Guid eventRecordSubscriptionId);
    }
}
