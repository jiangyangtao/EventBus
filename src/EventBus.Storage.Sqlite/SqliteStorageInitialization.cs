using EventBus.Storage.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.Sqlite
{
    internal class SqliteStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {

        }
    }
}
