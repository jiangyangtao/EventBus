using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class ApplicationManager : BaseRepository<Application>, IApplicationManager
    {

        private readonly IApplicationProvider _applicationProvider;

        public ApplicationManager(IRepository repository, IApplicationProvider applicationProvider) : base(repository)
        {
            _applicationProvider = applicationProvider;
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
            endpoint.NoticeProtocol = applicationEndpoint.NoticeProtocol;
            endpoint.FailedRetryPolicy = applicationEndpoint.FailedRetryPolicy;

            await UpdateAsync(endpoint);
        }

        public async Task RemoveApplicationAsync(IApplication application)
        {
            var app = await _applicationProvider.GetApplicationAsync(application.Id);
            if (app != null)
            {
                await DeleteAsync(a => a.Id == application.Id);
            }
        }

        public async Task RemoveApplicationEndpointAsync(IApplicationEndpoint endpoint)
        {
            var applicationEndpoint = await _applicationProvider.GetApplicationEndpointAsync(endpoint.Id);
            if (applicationEndpoint != null)
            {
                await DeleteAsync(a => a.Id == endpoint.Id);
            }
        }
    }
}
