using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class Subscription : BaseEntity<Subscription>, Abstractions.IModels.Subscription
    {
        public Guid EvnetId { get; set; }

        [NotMapped]
        public Abstractions.IModels.Event Event { get; set; }

        public Guid ApplicationEndpointId { get; set; }

        [NotMapped]
        public Abstractions.IModels.ApplicationEndpoint ApplicationEndpoint { get; set; }
    }
}
