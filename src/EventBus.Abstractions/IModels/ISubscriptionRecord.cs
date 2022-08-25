using EventBus.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 订阅记录
    /// </summary>
    public interface ISubscriptionRecord : IBaseModel
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
        /// 通知协议
        /// </summary>
        public ProtocolType NoticeProtocol { get; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString { get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, object> Header { get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// 是否通知成功，true 成功，false 失败
        /// </summary>
        public bool NoticeResult { get; }

        /// <summary>
        /// 订阅
        /// </summary>
        public ISubscription Subscription { get; }

        /// <summary>
        /// 接入点的订阅记录
        /// </summary>
        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { get; }
    }
}
