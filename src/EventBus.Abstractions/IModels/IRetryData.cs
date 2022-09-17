

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 重试
    /// </summary>
    public interface IRetryData : IBaseModel
    {
        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { get; }

        /// <summary>
        /// 订阅的分组日志 Id
        /// </summary>
        public Guid EventRecordSubscriptionId { get; }

        public IEventRecord EventRecord { get; }

        public IEvent Event { get; }

        public IEventRecordSubscription EventRecordSubscription { get; }
    }
}
