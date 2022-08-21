using EventBus.Abstractions.IProviders;
using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Core.Base;

namespace EventBus.Core.Providers
{
    internal class ApplicationManager : BaseService, IApplicationManager
    {

        private readonly IApplicationProvider _applicationProvider;

        public ApplicationManager(IRepository repository, IApplicationProvider applicationProvider) : base(repository)
        {
            _applicationProvider = applicationProvider;
        }

        public Task AddOrUpdateApplicationAsync(IApplication application)
        {
            throw new NotImplementedException();
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
