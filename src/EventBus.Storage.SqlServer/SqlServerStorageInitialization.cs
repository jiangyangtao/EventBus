using EventBus.Storage.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.SqlServer
{
    internal class SqlServerStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {

        }
    }
}
