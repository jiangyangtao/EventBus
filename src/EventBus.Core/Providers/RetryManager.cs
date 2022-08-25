using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class RetryManager : BaseRepository<RetryData>, IRetryManager
    {
        private readonly IRetryProvider _retryProvider;

        public RetryManager(IRepository repository, IRetryProvider retryProvider) : base(repository)
        {
            _retryProvider = retryProvider;
        }

        public async Task RetryAsync()
        {
            // TODO Realize get retry data to retry queue
            await Task.CompletedTask;
        }

        public Task RetryAsync(Guid retryDataId)
        {
            throw new NotImplementedException();
        }
    }
}
