using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Extensions;
using EventBus.Infrastructure.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class ApplicationProvider : BaseService, IApplicationProvider
    {
        public ApplicationProvider(IRepository repository) : base(repository)
        {
        }

        public async Task<IApplication> GetApplicationAsync(Guid applicationId)
        {
            var application = await _repository.Get<Application>().FirstOrDefaultAsync(a => a.Id == applicationId);
            if (application == null) return null;

            return application;
        }

        public async Task<IApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId)
        {
            var applicationEndpoint = await _repository.Get<ApplicationEndpoint>().FirstOrDefaultAsync(a => a.Id == applicationEndpointId);
            if (applicationEndpoint == null) return null;

            return applicationEndpoint;
        }

        public async Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId)
        {
            var applicationEndpoints = await _repository.Get<ApplicationEndpoint>().Where(a => a.ApplicationId == applicationId).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return ApplicationEndpoint.EmptyArray;

            return applicationEndpoints;
        }

        public async Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName)
        {
            var query = _repository.Get<ApplicationEndpoint>();
            if (endpointName.NotNullAndEmpty()) query.Where(a => a.EndpointName.Contains(endpointName));

            var applicationEndpoints = await query.Skip(start).Take(count).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return ApplicationEndpoint.EmptyArray;

            return applicationEndpoints;
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
