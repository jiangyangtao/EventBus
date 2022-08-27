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

        public RetryPolicy(int intervalTime, RetryBehavior behavior)
        {
            IntervalTime = intervalTime;
            Behavior = behavior;
        }

        public int IntervalTime { get; set; }

        public RetryBehavior Behavior { get; set; }
    }
}
