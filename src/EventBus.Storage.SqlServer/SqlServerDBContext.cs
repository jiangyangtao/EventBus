using EventBus.Storage.Abstractions.Enums;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.SqlServer
{
    internal class SqlServerDBContext : IDataBaseContext
    {
        public StorageType StorageType => StorageType.SqlServer;
    }
}
