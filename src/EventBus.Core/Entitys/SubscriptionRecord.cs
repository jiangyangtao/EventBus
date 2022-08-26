using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class SubscriptionRecord : BaseEntity<SubscriptionRecord>, ISubscriptionRecord
    {
        public SubscriptionRecord() { }

        public SubscriptionRecord(ISubscription a)
        {            
            //EndpointName = a.end
        }

        public string EndpointName { set; get; }

        public Uri EndpointUrl { set; get; }

        public ProtocolType NoticeProtocol { set; get; }

        public string QueryString { set; get; }

        public IDictionary<string, object> Header { set; get; }

        public object Data { set; get; }

        public bool NoticeResult { set; get; }

        public ISubscription Subscription { set; get; }

        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { set; get; }
    }
}
