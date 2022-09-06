using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;

namespace EventBus.Application.Dto
{
    public class RetryPolicyDto : IRetryPolicy
    {
        public RetryPolicyDto(IRetryPolicy retryPolicy)
        {
            RetryDelayCount = retryPolicy.RetryDelayCount;
            RetryDelayUnit = retryPolicy.RetryDelayUnit;
            Behavior = retryPolicy.Behavior;
        }

        public RetryDelayUnit RetryDelayUnit { set; get; }

        public int RetryDelayCount { set; get; }

        public RetryBehavior Behavior { set; get; }
    }
}
