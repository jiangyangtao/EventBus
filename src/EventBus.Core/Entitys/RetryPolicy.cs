using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Entitys
{
    internal class RetryPolicy : IRetryPolicy
    {
        public RetryPolicy()
        {
        }

        public RetryPolicy(int retryDelayCount, RetryBehavior behavior)
        {
            RetryDelayCount = retryDelayCount;
            Behavior = behavior;
        }

        public RetryDelayUnit RetryDelayUnit { get; set; } = RetryDelayUnit.Second;

        /// <summary>
        /// 间隔总量
        /// </summary>
        public int RetryDelayCount { set; get; }

        /// <summary>
        /// 重试行为
        /// </summary>
        public RetryBehavior Behavior { set; get; }
    }
}
