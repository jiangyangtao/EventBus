using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.MySql
{
    internal class MySqlStorageInitialization : IStorageInitialization
    {
        public StorageType StorageType => StorageType.MySql;

        public void Initialize(IServiceCollection services, string connectionString)
        {

        }
    }
}
