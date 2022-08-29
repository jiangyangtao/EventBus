using EventBus.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 重试策略
    /// </summary>
    public interface IRetryPolicy
    {
        /// <summary>
        /// 重试延迟单位
        /// </summary>
        public RetryDelayUnit RetryDelayUnit { get;  }

        /// <summary>
        /// 重试延迟量
        /// </summary>
        public int RetryDelayCount {get; }

        /// <summary>
        /// 重试行为
        /// </summary>
        public RetryBehavior Behavior {get; }
    }
}
