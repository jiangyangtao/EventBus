using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{

    internal class RetryData: BaseEntity, IRetryData
    {
        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { set; get; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetriesCount { set; get; }

        /// <summary>
        /// 订阅的分组日志 Id
        /// </summary>
        public Guid SubscriptionGroupLogId { set; get; }
    }
}
