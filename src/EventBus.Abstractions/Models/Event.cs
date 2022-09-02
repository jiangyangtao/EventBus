using EventBus.Abstractions.Enums;
using System.Net;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event : BaseModel
    {
        /// <summary>
        /// 事件 Id
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { set; get; }

        /// <summary>
        /// 是否启用IP地址白名单
        /// </summary>
        public bool EnableIPAddressWhiteList { set; get; }

        /// <summary>
        /// IP地址白名单
        /// </summary>
        public string[] IPAddressWhiteList { set; get; }

        /// <summary>
        /// 事件的协议
        /// </summary>
        public ProtocolType EventProtocol { set; get; }

        /// <summary>
        /// 订阅集
        /// </summary>
        public Subscription[] Subscriptions { set; get; }

        public bool VerifyIPAddress(IPAddress address)
        {
            if (EnableIPAddressWhiteList == false) return true;

            return IPAddressWhiteList.Any(a => a == address.ToString());
        }
    }
}
