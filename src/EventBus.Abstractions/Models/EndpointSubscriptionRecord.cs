using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 接入点的订阅记录
    /// </summary>
    public class EndpointSubscriptionRecord
    {
        /// <summary>
        /// 接入点的订阅记录 Id
        /// </summary>
        public Guid EndpointSubscriptionRecordId { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public SubscriptionType SubscriptionType { set; get; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime SubscriptionTime { set; get; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { set; get; }

        /// <summary>
        /// 是否成功状态码
        /// </summary>
        public bool IsSuccessStatusCode { set; get; }

        /// <summary>
        /// 响应的状态码
        /// </summary>
        public string ResponseStatucCode { set; get; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public object ResponseContent { set; get; }

        /// <summary>
        /// 耗时，单位：秒
        /// </summary>
        public long UsageTime { set; get; }


        public Subscription Subscription { set; get; }
    }
}
