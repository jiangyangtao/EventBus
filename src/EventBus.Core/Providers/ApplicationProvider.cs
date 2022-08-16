using EventBus.Domain.IModels;
using EventBus.Domain.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class ApplicationProvider : IApplicationProvider
    {
        public Task<IApplication> GetApplicationAsync(string applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IApplicationEndpoint> GetApplicationEndpointAsync(string applicationEndpointId)
        {
            throw new NotImplementedException();
        }

        public Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(string applicationId)
        {
            throw new NotImplementedException();
        }

        public Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName)
        {
            throw new NotImplementedException();
        }

        public Task<IApplication[]> GetApplicationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IApplication[]> GetApplicationsAsync(int start, int count, string applicationName)
        {
            throw new NotImplementedException();
        }
    }
}
