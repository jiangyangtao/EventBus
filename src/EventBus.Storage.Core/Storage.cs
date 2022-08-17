using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Core
{
    internal class Storage
    {
        public string ConnectionString { set; get; }

        public StorageType StorageType { set; get; }
    }

    internal enum StorageType
    {
        MySql,
        Sqlite,
        SqlServer,
        MongoDB
    }
}
