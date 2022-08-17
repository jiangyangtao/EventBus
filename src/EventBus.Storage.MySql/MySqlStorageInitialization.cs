using EventBus.Storage.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.MySql
{
    internal class MySqlStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {

        }
    }
}
