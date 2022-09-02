using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System.Net;

namespace EventBus.API.Dto
{
    public class EventDto : IEvent
    {
        public EventDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public string EventName => throw new NotImplementedException();

        public bool EnableIPAddressWhiteList => throw new NotImplementedException();

        public string[] IPAddressWhiteList => throw new NotImplementedException();

        public ProtocolType EventProtocol => throw new NotImplementedException();

        public ISubscription[] Subscriptions => throw new NotImplementedException();

        public DateTime CreateTime => throw new NotImplementedException();

        public DateTime UpdateTime => throw new NotImplementedException();

        public ISubscriptionRecord[] BuilderSubscriptionRecords(IEventRecord eventRecord)
        {
            throw new NotImplementedException();
        }

        public bool VerifyIPAddress(IPAddress address)
        {
            throw new NotImplementedException();
        }
    }
}
