using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;

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
            var data = await _applicationProvider.GetApplicationAsync(application.Id);
            if (data == null)
            {
                await CreateAsync(new Application()
                {
                    ApplicationName = application.ApplicationName,
                });
                return;
            }


            var app = new Application(data)
            {
                ApplicationName = application.ApplicationName
            };
            await UpdateAsync(app);
        }

        public Task AddOrUpdateApplicationEndpointAsync(IApplicationEndpoint applicationEndpoint)
        {
            throw new NotImplementedException();
        }

        public Task RemoveApplicationAsync(IApplication application)
        {
            throw new NotImplementedException();
        }

        public Task RemoveApplicationEndpointAsync(IApplicationEndpoint endpoint)
        {
            throw new NotImplementedException();
        }
    }
}
