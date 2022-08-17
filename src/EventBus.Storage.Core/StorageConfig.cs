using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Core
{
    internal class StorageConfig
    {
        public StorageConfig(IConfiguration configuration)
        {

        }

        public StorageType StorageType { set; get; }

        public string ConnectionString { set; get; }
    }
}
