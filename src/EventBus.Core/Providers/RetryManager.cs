using EventBus.Abstractions.IProviders;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class RetryManager : BaseService, IRetryManager
    {
        public RetryManager(IRepository repository) : base(repository)
        {
        }

        public Task RetryAsync(string retryDataId)
        {
            throw new NotImplementedException();
        }
    }
}
