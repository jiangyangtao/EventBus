
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class Event : BaseEntity<Event>, IEvent
    {
        public string EventName { set; get; }

        public bool EnableIPAddressWhiteList { set; get; }

        public string[] IPAddressWhiteList { set; get; }

        public ProtocolType EventProtocol { set; get; }

        public IEventLog[] EventLogs { set; get; }

        public ISubscription[] Subscriptions { set; get; }
    }
}
