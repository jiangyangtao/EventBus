using EventBus.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 接入点的订阅记录
    /// </summary>
    public interface IEndpointSubscriptionRecord
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public SubscriptionType SubscriptionType { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime SubscriptionTime { get; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { get; }

        /// <summary>
        /// 是否成功状态码
        /// </summary>
        public bool IsSuccessStatusCode { set; get; }

        /// <summary>
        /// 响应的状态码
        /// </summary>
        public string ResponseStatucCode { get; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public object ResponseContent { get; }

        /// <summary>
        /// 耗时，单位：秒
        /// </summary>
        public long UsageTime { get; }


        public ISubscription Subscription { get; }
    }
}
