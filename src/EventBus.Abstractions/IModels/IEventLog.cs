using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.IModels
{
    /// <summary>
    /// 事件日志
    /// </summary>
    public interface IEventLog : IBaseModel
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString{ get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public Dictionary<string, object> Header { get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurrenceTime { get; }

        /// <summary>
        /// 订阅完成率
        /// </summary>
        public decimal SubscriptionCompletionRate { get; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// 订阅的分组日志
        /// </summary>
        public ISubscriptionGroupLog[] ISubscriptionGroupLogs { get; }

    }
}
