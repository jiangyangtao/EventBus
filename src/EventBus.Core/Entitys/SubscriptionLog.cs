using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class SubscriptionLog : BaseEntity<SubscriptionLog>, ISubscriptionLog
    {
        public NoticeType NoticeType { get; set; } = NoticeType.Automatic;

        public DateTime NoticeTime { set; get; }

        public DateTime ResponseTime { set; get; }

        public string ResponseStatucCode { set; get; }

        public object ResponseContent { set; get; }

        public int UsageTime { set; get; }

        public ISubscription Subscription { set; get; }

        public IEventLog EventLog { set; get; }
    }
}
