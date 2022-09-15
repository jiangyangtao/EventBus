using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Application.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class SubscriptionDtoBase
    {
        [Required]
        public Guid SubscriptionId { get; set; }
    }

    public class SubscriptionDto : ISubscription
    {
        public string EndpointName { get; set; }

        public Uri EndpointUrl { get; set; }

        public ProtocolType SubscriptionProtocol { get; set; }

        public int RequestTimeout { get; set; }

        public IRetryPolicy[] FailedRetryPolicy { get; set; }

        public Guid EventId { get; set; }

        public IEvent Event { get; set; }

        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public class SubscriptionAddDto
    {
        public Guid EventId { set; get; }

        public Guid ApplicationEndpointId { set; get; }
    }

    public class SubscriptionModifyDto
    {
        public Guid EventId { set; get; }

        [Required, MaxLength(100)]
        public string EndpointName { set; get; }

        [UriValidation]
        public string EndpointUrl { set; get; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ProtocolType? SubscriptionProtocol { set; get; }

        public int RequestTimeout { set; get; }

        public RetryPolicyDto[] FailedRetryPolicy { set; get; }

        private SubscriptionDto GetSubscription()
        {
            return new SubscriptionDto
            {
                EndpointName = EndpointName,
                EndpointUrl = new Uri(EndpointUrl),
                SubscriptionProtocol = SubscriptionProtocol.Value,
                RequestTimeout = RequestTimeout,
                EventId = EventId,
                FailedRetryPolicy = FailedRetryPolicy,
            };
        }

        internal ISubscription BuildSubscription() => GetSubscription();

        internal ISubscription BuildSubscription(Guid subscriptionId)
        {
            var subscription = GetSubscription();
            subscription.Id = subscriptionId;

            return subscription;
        }
    }

    public class SubscriptionQueryDto : PagingParameter
    {
        public Guid? EventId { set; get; }

        public string EndpointName { set; get; }
    }

    public class SubscriptionResult : SubscriptionModifyDto
    {
        public SubscriptionResult(ISubscription subscription)
        {
            SubscriptionId = subscription.Id;
            EndpointName = subscription.EndpointName;
            EndpointUrl = subscription.EndpointUrl.ToString();
            SubscriptionProtocol = subscription.SubscriptionProtocol;
            RequestTimeout = subscription.RequestTimeout;
            FailedRetryPolicy = subscription.FailedRetryPolicy.Select(a => new RetryPolicyDto(a)).ToArray();

            if (subscription.Event != null)
            {
                EventId = subscription.Event.Id;
                EventName = subscription.Event.EventName;
            }
        }

        public SubscriptionResult(ISubscription subscription, IEvent e) : this(subscription)
        {
            if (e != null)
            {
                EventId = e.Id;
                EventName = e.EventName;
            }
        }

        /// <summary>
        /// 事件 Id
        /// </summary>
        public Guid SubscriptionId { set; get; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { set; get; }
    }

    public class SubscriptionPaginationResult : PaginationResult<SubscriptionResult>
    {
        public SubscriptionPaginationResult(long count, ISubscription[] subscriptions) : base(count)
        {
            List = subscriptions.Select(a => new SubscriptionResult(a)).ToArray();
        }
    }
}
