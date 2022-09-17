

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 事件记录
    /// </summary>
    public interface IEventRecord : IBaseModel
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString { get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, string> Header { get; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; }

        /// <summary>
        /// 客户端 IP 地址
        /// </summary>
        public string ClientIPAddress { get; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; }

        /// <summary>
        /// 订阅完成率
        /// </summary>
        public decimal SubscriptionCompletionRate { get; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// 事件的订阅记录
        /// </summary>
        public IEventRecordSubscription[] EventRecordSubscriptions { get; }

        public HttpContent BuilderHttpContent();

    }
}
