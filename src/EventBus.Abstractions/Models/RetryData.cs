using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 重试
    /// </summary>
    public class RetryData : BaseModel
    {
        /// <summary>
        /// 重试 Id
        /// </summary>
        public Guid RetryDataId { set; get; }

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
