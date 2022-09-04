using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;

namespace EventBus.Core.Entitys
{
    public class RetryPolicy : IRetryPolicy
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

        public int RetryDelayCount { set; get; }

        public RetryBehavior Behavior { set; get; }
    }
}
