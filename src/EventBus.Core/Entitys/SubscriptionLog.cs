using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class SubscriptionLog : BaseEntity, ISubscriptionLog
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
