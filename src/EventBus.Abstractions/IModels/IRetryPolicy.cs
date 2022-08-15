using EventBus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.IModels
{
    /// <summary>
    /// 重试策略
    /// </summary>
    public interface IRetryPolicy
    {
        /// <summary>
        /// 间隔时间，单位：分钟
        /// </summary>
        public int IntervalTime { set; get; }

        /// <summary>
        /// 重试行为
        /// </summary>
        public RetryBehavior Behavior { set; get; }
    }
}
