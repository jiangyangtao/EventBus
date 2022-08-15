using EventBus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.IModels
{
    /// <summary>
    /// 订阅日志
    /// </summary>
    public interface ISubscriptionLog : IBaseModel
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public NoticeType NoticeType { get; set; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime NoticeTime { get; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { get; }

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
        public int UsageTime { get; }


        public ISubscription Subscription { get; }

        public IEventLog EventLog { get; }
    }
}
