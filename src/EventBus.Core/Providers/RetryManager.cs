using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
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
            var datas = await Get(a => a.RetryTime <= DateTime.Now).ToArrayAsync();
            // TODO 放入队列
        }

        public async Task RetryAsync(Guid retryDataId)
        {
            var data = await GetByIdAsync(retryDataId);
            // TODO 放入队列
        }
    }
}
