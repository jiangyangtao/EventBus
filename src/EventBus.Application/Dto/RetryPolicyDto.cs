using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBus.Application.Dto
{
    public class RetryPolicyDto : IRetryPolicy
    {
        public RetryPolicyDto() { }

        public RetryPolicyDto(IRetryPolicy retryPolicy)
        {
            RetryDelayCount = retryPolicy.RetryDelayCount;
            RetryDelayUnit = retryPolicy.RetryDelayUnit;
            Behavior = retryPolicy.Behavior;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public RetryDelayUnit RetryDelayUnit { set; get; }

        public int RetryDelayCount { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RetryBehavior Behavior { set; get; }
    }
}
