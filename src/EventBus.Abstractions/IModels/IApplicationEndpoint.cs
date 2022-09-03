using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 应用接入点
    /// </summary>
    public interface IApplicationEndpoint : IBaseModel
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
        /// 应用Id
        /// </summary>
        public Guid ApplicationId { get; }

        /// <summary>
        /// 所属应用
        /// </summary>
        public IApplication Application { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { get; }
    }

    public interface IRetryPolicy
    {
        /// <summary>
        /// 重试延迟单位
        /// </summary>
        public RetryDelayUnit RetryDelayUnit { get; }

        /// <summary>
        /// 重试延迟量
        /// </summary>
        public int RetryDelayCount { get; }

        /// <summary>
        /// 重试行为
        /// </summary>
        public RetryBehavior Behavior { get; }
    }
}
