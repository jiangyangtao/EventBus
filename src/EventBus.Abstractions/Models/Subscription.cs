

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 订阅
    /// </summary>
    public class Subscription : BaseModel
    {
        /// <summary>
        /// 订阅 Id
        /// </summary>
        public Guid SubscriptionId { set; get; }

        /// <summary>
        /// 事件
        /// </summary>
        public Event Event { get;  }

        /// <summary>
        /// 应用订阅的接入点
        /// </summary>
        public ApplicationEndpoint ApplicationEndpoint { get; }
    }
}
