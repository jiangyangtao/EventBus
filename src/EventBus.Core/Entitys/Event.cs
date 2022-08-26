
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;

namespace EventBus.Core.Entitys
{
    internal class Event : BaseEntity<Event>, IEvent
    {
        public Event()
        {
        }

        public Event(IEvent e)
        {
            EventName = e.EventName;
            EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            IPAddressWhiteList = e.IPAddressWhiteList;
            EventProtocol = e.EventProtocol;
            Subscriptions = e.Subscriptions;
        }


        public string EventName { set; get; }

        public bool EnableIPAddressWhiteList { set; get; }

        public string[] IPAddressWhiteList { set; get; }

        public ProtocolType EventProtocol { set; get; }

        public ISubscription[] Subscriptions { set; get; }

        public ISubscriptionRecord[] BuilderSubscriptionRecords()
        {
            if (Subscriptions.IsNullOrEmpty()) return SubscriptionRecord.EmptyArray;

            return Subscriptions.Select(a => new SubscriptionRecord(a)).ToArray();
        }
    }
}
