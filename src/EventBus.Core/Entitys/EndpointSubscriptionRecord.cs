using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class EndpointSubscriptionRecord : BaseEntity<EndpointSubscriptionRecord>, IEndpointSubscriptionRecord
    {
        public EndpointSubscriptionRecord()
        {
        }

        public NoticeType NoticeType { get; set; } = NoticeType.Automatic;

        public DateTime NoticeTime { set; get; }

        public DateTime ResponseTime { set; get; }

        public string ResponseStatucCode { set; get; }

        public object ResponseContent { set; get; }

        public long UsageTime { set; get; }

        /// <summary>
        /// 订阅
        /// </summary>
        [NotMapped]
        public ISubscription Subscription { get; }
    }
}
