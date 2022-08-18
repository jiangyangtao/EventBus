using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class Event : BaseEntity, IEvent
    {
        public string EventName { set; get; }

        public bool EnableIPAddressWhiteList { set; get; }

        public string[] IPAddressWhiteList { set; get; }

        public ProtocolType EventProtocol { set; get; }

        public IEventLog[] EventLogs { set; get; }

        public ISubscription[] Subscriptions { set; get; }
    }
}
