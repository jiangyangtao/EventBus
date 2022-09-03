using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class EndpointSubscriptionRecord : BaseEntity<EndpointSubscriptionRecord>, IEndpointSubscriptionRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid SubscriptionRecordId { set; get; }

        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Automatic;

        public DateTime SubscriptionTime { set; get; }

        public DateTime ResponseTime { set; get; }

        public bool IsSuccessStatusCode { set; get; }

        public string ResponseStatucCode { set; get; }

        public object ResponseContent { set; get; }

        public long UsageTime { set; get; }

        /// <summary>
        /// 订阅
        /// </summary>
        [NotMapped]
        public ISubscription Subscription { set; get; }
    }
}
