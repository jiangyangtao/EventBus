using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class Subscription : BaseEntity<Subscription>, ISubscription
    {
        public Guid EvnetId { get; set; }

        [NotMapped]
        public IEvent Event { get; set; }

        public Guid ApplicationEndpointId { get; set; }

        [NotMapped]
        public IApplicationEndpoint ApplicationEndpoint { get; set; }
    }
}
