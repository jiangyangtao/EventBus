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
    internal class RetryManager : BaseRepository, IRetryManager
    {
        private readonly IRetryProvider _retryProvider;

        public RetryManager(IRepository repository, IRetryProvider retryProvider) : base(repository)
        {
            _retryProvider = retryProvider;
        }

        public Task RetryAsync(string retryDataId)
        {
            throw new NotImplementedException();
        }
    }
}
