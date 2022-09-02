using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class ApplicationProvider : BaseRepository<Entitys.Application>, IApplicationProvider
    {
        public ApplicationProvider(IRepository repository) : base(repository)
        {
        }

        public async Task AddOrUpdateApplicationAsync(Abstractions.IModels.Application application)
        {
            var data = await GetByIdAsync(application.Id);
            if (data == null)
            {
                await CreateAsync(new Entitys.Application()
                {
                    ApplicationName = application.ApplicationName,
                });
                return;
            }

            data.ApplicationName = application.ApplicationName;
            await UpdateAsync(data);
        }

        public async Task AddOrUpdateApplicationEndpointAsync(Abstractions.IModels.ApplicationEndpoint applicationEndpoint)
        {
            var endpoint = await GetByIdAsync<Entitys.ApplicationEndpoint>(applicationEndpoint.Id);
            if (endpoint == null)
            {
                await CreateAsync(new Entitys.ApplicationEndpoint(applicationEndpoint));
            }

            endpoint.EndpointName = applicationEndpoint.EndpointName;
            endpoint.EndpointUrl = applicationEndpoint.EndpointUrl;
            endpoint.SubscriptionProtocol = applicationEndpoint.SubscriptionProtocol;
            endpoint.FailedRetryPolicy = applicationEndpoint.FailedRetryPolicy;

            await UpdateAsync(endpoint);
        }

        public async Task RemoveApplicationAsync(Abstractions.IModels.Application application)
        {
            var app = await GetApplicationAsync(application.Id);
            if (app != null)
            {
                await DeleteAsync(a => a.Id == application.Id);
            }
        }

        public async Task RemoveApplicationEndpointAsync(Abstractions.IModels.ApplicationEndpoint endpoint)
        {
            var applicationEndpoint = await GetApplicationEndpointAsync(endpoint.Id);
            if (applicationEndpoint != null)
            {
                await DeleteAsync(a => a.Id == endpoint.Id);
            }
        }


        public async Task<Abstractions.IModels.Application> GetApplicationAsync(Guid applicationId)
        {
            var application = await GetByIdAsync(applicationId);
            if (application == null) return null;

            return application;
        }

        public async Task<Abstractions.IModels.Application> GetApplicationAsync(string applicationName)
        {
            var application = await Get(a => a.ApplicationName == applicationName).FirstOrDefaultAsync();
            if (application == null) return null;

            return application;
        }

        public async Task<Abstractions.IModels.ApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId)
        {
            var applicationEndpoint = await GetByIdAsync<Entitys.ApplicationEndpoint>(applicationEndpointId);
            if (applicationEndpoint == null) return null;

            return applicationEndpoint;
        }

        public async Task<Abstractions.IModels.ApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId)
        {
            var applicationEndpoints = await Get<ApplicationEndpoint>().Where(a => a.ApplicationId == applicationId).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return Entitys.ApplicationEndpoint.EmptyArray;

            foreach (var applicationEndpoint in applicationEndpoints)
            {
                applicationEndpoint.Application = new Entitys.Application() { Id = applicationEndpoint.ApplicationId };
            }

            return applicationEndpoints;
        }

        public async Task<Abstractions.IModels.ApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName)
        {
            var query = Get<Entitys.ApplicationEndpoint>();
            if (endpointName.NotNullAndEmpty()) query.Where(a => a.EndpointName.Contains(endpointName));

            var applicationEndpoints = await query.Skip(start).Take(count).ToArrayAsync();
            if (applicationEndpoints.IsNullOrEmpty()) return Entitys.ApplicationEndpoint.EmptyArray;

            return applicationEndpoints;
        }

        public async Task<Abstractions.IModels.Application[]> GetApplicationsAsync()
        {
            var applications = await Get().ToArrayAsync();
            if (applications.IsNullOrEmpty()) return Entitys.Application.EmptyArray;

            return applications;
        }

        public async Task<Abstractions.IModels.Application[]> GetApplicationsAsync(int start, int count, string applicationName)
        {
            var query = Get();
            if (applicationName.NotNullAndEmpty()) query.Where(a => a.ApplicationName.Contains(applicationName));

            var applications = await query.Skip(start).Take(count).ToArrayAsync();
            if (applications.IsNullOrEmpty()) return Entitys.Application.EmptyArray;

            return applications;
        }
    }
}
