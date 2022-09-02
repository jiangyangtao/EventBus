using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class ApplicationProvider : BaseRepository<Application>, IApplicationProvider
    {
        public ApplicationProvider(IRepository repository) : base(repository)
        {
        }

        public async Task AddOrUpdateApplicationAsync(IApplication application)
        {
            var data = await GetByIdAsync(application.Id);
            if (data == null)
            {
                await CreateAsync(new Application()
                {
                    ApplicationName = application.ApplicationName,
                });
                return;
            }

            data.ApplicationName = application.ApplicationName;
            await UpdateAsync(data);
        }

        public async Task AddOrUpdateApplicationEndpointAsync(IApplicationEndpoint applicationEndpoint)
        {
            var endpoint = await GetByIdAsync<ApplicationEndpoint>(applicationEndpoint.Id);
            if (endpoint == null)
            {
                await CreateAsync(new ApplicationEndpoint(applicationEndpoint));
            }

            endpoint.EndpointName = applicationEndpoint.EndpointName;
            endpoint.EndpointUrl = applicationEndpoint.EndpointUrl;
            endpoint.SubscriptionProtocol = applicationEndpoint.SubscriptionProtocol;
            endpoint.FailedRetryPolicy = applicationEndpoint.FailedRetryPolicy;

            await UpdateAsync(endpoint);
        }

        public async Task RemoveApplicationAsync(IApplication application)
        {
            var app = await GetApplicationAsync(application.Id);
            if (app != null)
            {
                await DeleteAsync(a => a.Id == application.Id);
            }
        }

        public async Task RemoveApplicationEndpointAsync(IApplicationEndpoint endpoint)
        {
            var applicationEndpoint = await GetApplicationEndpointAsync(endpoint.Id);
            if (applicationEndpoint != null)
            {
                await DeleteAsync(a => a.Id == endpoint.Id);
            }
        }


        public async Task<IApplication> GetApplicationAsync(Guid applicationId)
        {
            var application = await GetByIdAsync(applicationId);
            if (application == null) return null;

            return application;
        }

        public async Task<IApplication> GetApplicationAsync(string applicationName)
        {
            var application = await Get(a => a.ApplicationName == applicationName).FirstOrDefaultAsync();
            if (application == null) return null;

            return application;
        }

        public async Task<IApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId)
        {
            var applicationEndpoint = await GetByIdAsync<ApplicationEndpoint>(applicationEndpointId);
            if (applicationEndpoint == null) return null;

            return applicationEndpoint;
        }

        public async Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId)
        {
            var applicationEndpoints = await Get<ApplicationEndpoint>().Where(a => a.ApplicationId == applicationId).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return ApplicationEndpoint.EmptyArray;

            foreach (var applicationEndpoint in applicationEndpoints)
            {
                applicationEndpoint.Application = new Application() { Id = applicationEndpoint.ApplicationId };
            }

            return applicationEndpoints;
        }

        public async Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName)
        {
            var query = Get<ApplicationEndpoint>();
            if (endpointName.NotNullAndEmpty()) query.Where(a => a.EndpointName.Contains(endpointName));

            var applicationEndpoints = await query.Skip(start).Take(count).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return ApplicationEndpoint.EmptyArray;

            return applicationEndpoints;
        }

        public async Task<IApplication[]> GetApplicationsAsync()
        {
            var applications = await Get().ToArrayAsync();
            if (applications.IsNullOrEmpty()) return Application.EmptyArray;

            return applications;
        }

        public async Task<IApplication[]> GetApplicationsAsync(int start, int count, string applicationName)
        {
            var query = Get();
            if (applicationName.NotNullAndEmpty()) query.Where(a => a.ApplicationName.Contains(applicationName));

            var applications = await query.Skip(start).Take(count).ToArrayAsync();
            if (applications.IsNullOrEmpty()) return Application.EmptyArray;

            return applications;
        }
    }
}
