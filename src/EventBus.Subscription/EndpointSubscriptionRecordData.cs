using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;

namespace EventBus.Subscription
{
    internal class EndpointSubscriptionRecordData : IEndpointSubscriptionRecord
    {
        public SubscriptionType SubscriptionType { get; set; }

        public DateTime SubscriptionTime { get; set; }

        public DateTime ResponseTime { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public string ResponseStatus { get; set; }

        public string ResponseStatusCode { get; set; }

        public IDictionary<string, string> ResponseHeaders { get; set; }

        public string ResponseContent { get; set; }

        public long UsageTime { get; set; }

        public ISubscription Subscription { get; set; }

        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public Guid EventRecordSubscriptionId { get; set; }
    }
}
