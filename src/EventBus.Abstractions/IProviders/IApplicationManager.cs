using EventBus.Domain.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IApplicationManager
    {
        Task AddOrUpdateApplicationAsync(IApplication application);

        Task AddOrUpdateApplicationEndpointAsync(IApplicationEndpoint applicationEndpoint);

        Task RemoveApplicationAsync(IApplication application);

        Task RemoveApplicationEndpointAsync(IApplicationEndpoint endpoint);
    }
}
