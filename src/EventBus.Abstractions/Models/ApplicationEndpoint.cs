using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 应用接入点
    /// </summary>
    public class ApplicationEndpoint : BaseModel
    {
        /// <summary>
        /// 应用接入点 Id
        /// </summary>
        public Guid ApplicationEndpointId { get; set; }

        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { set; get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { set; get; }

        /// <summary>
        /// 订阅协议
        /// </summary>
        public ProtocolType SubscriptionProtocol { set; get; }

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int RequestTimeout { set; get; }

        /// <summary>
        /// 应用Id
        /// </summary>
        public Guid ApplicationId { set; get; }

        /// <summary>
        /// 所属应用
        /// </summary>
        public Application Application { set; get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public RetryPolicy[] FailedRetryPolicy { set; get; }
    }

    public class RetryPolicy
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
