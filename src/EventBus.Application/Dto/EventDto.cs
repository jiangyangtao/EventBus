using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace EventBus.Application.Dto
{
    public class EventDtoBase
    {
        public Guid EventId { get; set; }
    }

    public class EventDto : IEvent
    {
        public string EventName { get; set; }

        public bool EnableIPAddressWhiteList { get; set; }

        public string[] IPAddressWhiteList { get; set; }

        public ProtocolType EventProtocol { get; set; }

        public ISubscription[] Subscriptions { get; set; }

        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public bool VerifyIPAddress(IPAddress address)
        {
            if (EnableIPAddressWhiteList == false) return true;

            return IPAddressWhiteList.Any(a => a == address.ToString());
        }
    }

    public class EventAddDto
    {
        [Required, MaxLength(50)]
        public string EventName { get; set; }

        public bool EnableIPAddressWhiteList { get; set; }

        public string[] IPAddressWhiteList { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProtocolType? EventProtocol { get; set; }

        private EventDto BuildEvent() => new EventDto
        {
            EventName = EventName,
            EventProtocol = EventProtocol.Value,
            EnableIPAddressWhiteList = EnableIPAddressWhiteList,
            IPAddressWhiteList = IPAddressWhiteList
        };

        internal IEvent GetEvent() => BuildEvent();

        internal IEvent GetEvent(Guid eventId)
        {
            var e = BuildEvent();
            e.Id = eventId;

            return e;
        }
    }

    public class EventQueryDto : PagingParameter
    {
        public string EventName { get; set; }
    }

    public class EventResult : EventAddDto
    {
        public EventResult(IEvent e)
        {
            EventId = e.Id;
            EventName = e.EventName;
            EventProtocol = e.EventProtocol;
            EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            IPAddressWhiteList = e.IPAddressWhiteList;
        }

        public Guid EventId { get; set; }
    }

    public class EventPaginationResult : PaginationResult<EventResult>
    {
        public EventPaginationResult(long count, IEvent[] events) : base(count)
        {
            List = events.Select(a => new EventResult(a)).ToArray();
        }
    }
}
