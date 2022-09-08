using EventBus.Abstractions.Enums;


namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 订阅
    /// </summary>
    public interface ISubscription : IBaseModel
    {
        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { get; }

        /// <summary>
        /// 订阅协议
        /// </summary>
        public ProtocolType SubscriptionProtocol { get; }

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int RequestTimeout { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { get; }

        public Guid EventId { get; set; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get; }
    }
}
