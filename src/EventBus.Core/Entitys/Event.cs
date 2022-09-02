
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace EventBus.Core.Entitys
{
    internal class Event : BaseEntity<Event>, Abstractions.IModels.Event
    {
        public Event()
        {
        }

        public Event(Abstractions.IModels.Event e)
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

        [NotMapped]
        public Abstractions.IModels.Subscription[] Subscriptions { set; get; }

        public Abstractions.IModels.SubscriptionRecord[] BuilderSubscriptionRecords(Abstractions.IModels.EventRecord eventRecord)
        {
            if (Subscriptions.IsNullOrEmpty()) return SubscriptionRecord.EmptyArray;

            return Subscriptions.Where(a => a.ApplicationEndpoint != null).Select(a => new SubscriptionRecord(Id, eventRecord, a.ApplicationEndpoint)).ToArray();
        }

        public bool VerifyIPAddress(IPAddress address)
        {
            if (EnableIPAddressWhiteList == false) return true;

            return IPAddressWhiteList.Any(a => a == address.ToString());
        }
    }
}
