using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    public interface IRetryData
    {
        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { get; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetriesCount { get; }

        /// <summary>
        /// 订阅的分组日志 Id
        /// </summary>
        public Guid SubscriptionGroupLogId { get; }
    }
}
