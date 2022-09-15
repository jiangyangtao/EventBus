using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    public class Subscription : BaseEntity<Subscription>, ISubscription
    {
        public Subscription()
        {
        }

        public Subscription(Guid evnetId, IApplicationEndpoint endpoint) : base()
        {
            EventId = evnetId;

            EndpointName = endpoint.EndpointName;
            EndpointUrl = endpoint.EndpointUrl;
            SubscriptionProtocol = endpoint.SubscriptionProtocol;
            RequestTimeout = endpoint.RequestTimeout;
            FailedRetryPolicy = endpoint.FailedRetryPolicy;
        }

        public Subscription(ISubscription subscription) : base()
        {
            EventId = subscription.EventId;

            EndpointName = subscription.EndpointName;
            EndpointUrl = subscription.EndpointUrl;
            SubscriptionProtocol = subscription.SubscriptionProtocol;
            RequestTimeout = subscription.RequestTimeout;
            FailedRetryPolicy = subscription.FailedRetryPolicy;
        }

        public Guid EventId { get; set; }

        [NotMapped]
        public IEvent Event { get; set; }

        public string EndpointName { get; set; }

        [NotMapped]
        public Uri EndpointUrl
        {
            set
            {
                EndpointUrlString = value.ToString();
            }
            get
            {
                if (EndpointUrlString == null) return null;

                return new Uri(EndpointUrlString);
            }
        }

        public string EndpointUrlString { set; get; }

        public ProtocolType SubscriptionProtocol { get; set; }

        public int RequestTimeout { get; set; }

        [NotMapped]
        public IRetryPolicy[] FailedRetryPolicy
        {
            set
            {
                FailedRetryPolicyContent = JsonConvert.SerializeObject(value);
            }

            get
            {
                if (FailedRetryPolicyContent.IsNullOrEmpty()) return Array.Empty<RetryPolicy>();

                return JsonConvert.DeserializeObject<RetryPolicy[]>(FailedRetryPolicyContent);
            }
        }

        public string FailedRetryPolicyContent { set; get; }
    }
}
