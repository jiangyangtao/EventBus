using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Services
{
    internal class SubscriptionQueueService : ISubscriptionQueueService
    {
        public Task PutAsync(IEndpointSubscriptionRecord endpointSubscriptionRecord)
        {
            throw new NotImplementedException();
        }

        public Task PutAsync(IEndpointSubscriptionRecord[] endpointSubscriptionRecords)
        {
            throw new NotImplementedException();
        }
    }
}
