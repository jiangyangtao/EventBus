using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class RetryProvider : BaseService, IRetryProvider
    {
        public RetryProvider(IRepository repository) : base(repository)
        {
        }

        public Task<IRetryData[]> GetEventRetryAsync(string evnetId)
        {
            throw new NotImplementedException();
        }

        public Task<IRetryData> GetRetryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IRetryData[]> GetToBeExecutedRetryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
