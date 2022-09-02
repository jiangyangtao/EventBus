using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 订阅记录
    /// </summary>
    public class SubscriptionRecord : BaseModel
    {
        /// <summary>
        /// 订阅记录 Id
        /// </summary>
        public Guid SubscriptionRecordId { set; get; }

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
        public RetryPolicy[] FailedRetryPolicy { get; }

        /// <summary>
        /// 订阅结果，true 成功，false 失败
        /// </summary>
        public bool SubscriptionResult { get; }

        /// <summary>
        /// 接入点的订阅记录
        /// </summary>
        public EndpointSubscriptionRecord[] EndpointSubscriptionRecords { get; }

        /// <summary>
        /// 获取重试策略
        /// </summary>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public RetryPolicy GetRetryPolicy(int retryCount = 1) { 
        
        }

        /// <summary>
        /// 获取订阅内容
        /// </summary>
        public HttpContent GetSubscriptionContent() { 
        
        }
    }
}
