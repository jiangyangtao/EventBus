using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Sqlite
{
    internal class SqliteStorageInitialization : IStorageInitialization
    {
        public StorageType StorageType => StorageType.Sqlite;

        public void Initialize(IServiceCollection services, string connectionString)
        {

        }
    }
}
