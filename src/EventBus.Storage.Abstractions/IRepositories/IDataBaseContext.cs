using EventBus.Storage.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Abstractions.IRepositories
{
    public interface IDataBaseContext
    {
        public StorageType StorageType { get; }
    }
}
