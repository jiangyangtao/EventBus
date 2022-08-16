using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.SqlServer
{
    internal class StorageInitialization : IStorageInitialization
    {
        public StorageType StorageType => StorageType.SqlServer;

        public void Initialize(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
