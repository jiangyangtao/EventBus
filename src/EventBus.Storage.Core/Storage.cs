

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
        SqlServer
    }
}
