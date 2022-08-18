using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class Subscription : BaseEntity, ISubscription
    {
        public Guid EvnetId { get; set; }

        public IEvent Event { get; set; }

        public Guid ApplicationEndpointId { get; set; }

        public IApplicationEndpoint ApplicationEndpoint { get; set; }
    }
}
