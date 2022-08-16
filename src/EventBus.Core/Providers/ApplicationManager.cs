using EventBus.Abstractions.IProviders;
using EventBus.Domain.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class ApplicationManager : BaseService, IApplicationManager
    {

        public ApplicationManager(IRepository repository) : base(repository)
        {

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
