

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 订阅
    /// </summary>
    public interface ISubscription : IBaseModel
    {
        /// <summary>
        /// 订阅 Id
        /// </summary>
        public Guid SubscriptionId {  get; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get;  }

        /// <summary>
        /// 应用订阅的接入点
        /// </summary>
        public IApplicationEndpoint ApplicationEndpoint { get; }
    }
}
