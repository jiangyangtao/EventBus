using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IService
{
    public interface ISubscriptionQueueService
    {
        Task PutAsync(IEndpointSubscriptionRecord endpointSubscriptionRecord);

        Task PutAsync(IEndpointSubscriptionRecord[] endpointSubscriptionRecords);
    }
}
