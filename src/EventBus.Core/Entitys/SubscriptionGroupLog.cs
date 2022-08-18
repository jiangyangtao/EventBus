using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class SubscriptionGroupLog : BaseEntity, ISubscriptionGroupLog
    {
        public string EndpointName { get; set; }

        public Uri EndpointUrl { get; set; }

        public ProtocolType NoticeProtocol { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, object> Header { get; set; }

        public object Data { get; set; }

        public bool NoticeResult { get; set; }

        public ISubscription Subscription { get; set; }

        public ISubscriptionLog[] SubscriptionLogs { get; set; }
    }
}
