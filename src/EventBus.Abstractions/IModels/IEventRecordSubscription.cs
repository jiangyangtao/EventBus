using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 事件记录的订阅
    /// </summary>
    public interface IEventRecordSubscription : IBaseModel
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
        /// 请求超时时间，单位：秒
        /// </summary>
        public int RequestTimeout { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { get; }

        /// <summary>
        /// 订阅结果，true 成功，false 失败
        /// </summary>
        public bool SubscriptionResult { get; }

        /// <summary>
        /// 接入点的订阅记录
        /// </summary>
        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { get; }
    }
}
