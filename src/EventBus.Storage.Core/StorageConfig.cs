using EventBus.Storage.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Core
{
    internal class StorageConfig
    {
        public StorageType StorageType { set; get; }

        public string ConnectionString { set; get; }
    }
}
