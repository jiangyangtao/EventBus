using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Storage.Abstractions
{
    public interface IStorageInitialization
    {
        void Initialize(IServiceCollection services, string connectionString);
    }
}
