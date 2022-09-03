using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 接入点的订阅记录
    /// </summary>
    public interface IEndpointSubscriptionRecord : IBaseModel
    {
        /// <summary>
        /// 通知类型
        /// </summary>
        public SubscriptionType SubscriptionType {  get; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime SubscriptionTime {  get; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime {  get; }

        /// <summary>
        /// 是否成功状态码
        /// </summary>
        public bool IsSuccessStatusCode { get; }

        /// <summary>
        /// 响应的状态码
        /// </summary>
        public string ResponseStatucCode { get; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public object ResponseContent {  get; }

        /// <summary>
        /// 耗时，单位：秒
        /// </summary>
        public long UsageTime {  get; }


        public ISubscription Subscription {  get; }
    }
}
