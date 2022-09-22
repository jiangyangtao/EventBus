

namespace EventBus.Abstractions.Enums
{
    /// <summary>
    /// 协议类型
    /// </summary>
    public enum ProtocolType
    {
        Http,
        gRPC,
    }

    public enum SubscriptionType
    {
        /// <summary>
        /// 自动
        /// </summary>
        Automatic,

        /// <summary>
        /// 手动
        /// </summary>
        Manual
    }
}
