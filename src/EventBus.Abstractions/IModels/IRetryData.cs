using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 重试
    /// </summary>
    public interface IRetryData : IBaseModel
    {
        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { get; }

        /// <summary>
        /// 订阅的分组日志 Id
        /// </summary>
        public Guid SubscriptionRecordId { get; }
    }
}
