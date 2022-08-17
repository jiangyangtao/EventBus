using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EventBus.Storage.MongoDB
{
    internal class MongoDBStorageInitialization : IStorageInitialization
    {
        public void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddSingleton(new MongoClient(connectionString));
            services.AddScoped<IRepository, MongoDBRepository>();
        }
    }
}
