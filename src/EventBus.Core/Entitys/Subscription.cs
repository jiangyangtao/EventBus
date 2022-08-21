using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class Subscription : BaseEntity<Subscription>, ISubscription
    {
        public Guid EvnetId { get; set; }

        public IEvent Event { get; set; }

        public Guid ApplicationEndpointId { get; set; }

        public IApplicationEndpoint ApplicationEndpoint { get; set; }
    }
}
