
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace EventBus.Core.Entitys
{
    public class Event : BaseEntity<Event>, IEvent
    {
        private readonly string ArraySeparator = ",";

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

        [NotMapped]
        public string[] IPAddressWhiteList
        {
            set
            {
                if (value.NotNullAndEmpty()) IPAddressWhiteListContent = string.Join(ArraySeparator, value);
                else IPAddressWhiteListContent = string.Empty;
            }
            get
            {
                if (IPAddressWhiteListContent.IsNullOrEmpty()) return Array.Empty<string>();

                return IPAddressWhiteListContent.Split(ArraySeparator);
            }
        }

        public string IPAddressWhiteListContent { set; get; }

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
