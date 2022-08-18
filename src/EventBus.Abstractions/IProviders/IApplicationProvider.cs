using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 应用提供者
    /// </summary>
    public interface IApplicationProvider
    {
        Task<IApplication> GetApplicationAsync(string applicationId);

        Task<IApplication[]> GetApplicationsAsync();

        Task<IApplication[]> GetApplicationsAsync(int start, int count, string applicationName);

        Task<IApplicationEndpoint> GetApplicationEndpointAsync(string applicationEndpointId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(string applicationId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName);
    }
}
