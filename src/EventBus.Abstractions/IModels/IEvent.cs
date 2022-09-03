using EventBus.Abstractions.Enums;
using System.Net;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 事件
    /// </summary>
    public interface IEvent : IBaseModel
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// 是否启用IP地址白名单
        /// </summary>
        public bool EnableIPAddressWhiteList { get; }

        /// <summary>
        /// IP地址白名单
        /// </summary>
        public string[] IPAddressWhiteList {get; }

        /// <summary>
        /// 事件的协议
        /// </summary>
        public ProtocolType EventProtocol {  get; }

        /// <summary>
        /// 订阅集
        /// </summary>
        public ISubscription[] Subscriptions { get; }

        public bool VerifyIPAddress(IPAddress address)
        {
            if (EnableIPAddressWhiteList == false) return true;

            return IPAddressWhiteList.Any(a => a == address.ToString());
        }
    }
}
