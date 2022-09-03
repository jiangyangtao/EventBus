
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

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

        [NotMapped]
        public ISubscription[] Subscriptions { set; get; }

        public bool VerifyIPAddress(IPAddress address)
        {
            if (EnableIPAddressWhiteList == false) return true;

            return IPAddressWhiteList.Any(a => a == address.ToString());
        }
    }
}
