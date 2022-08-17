using EventBus.Extensions;
using EventBus.Storage.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EventBus.Storage.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            var storageSection = configuration.GetSection(nameof(Storage));
            var storageTypeValue = storageSection.GetSection(nameof(Storage.StorageType)).Value;
            var r = Enum.TryParse(storageTypeValue, true, out StorageType storageType);
            if (r == false)
            {
                storageType = StorageType.Sqlite;
                logger.LogError($"Unrecognizable StorageType:{storageTypeValue}. Enable default database:Sqlite.");
            }

            logger.LogInformation($"Database: {storageType}");

            var connectionString = storageSection.GetSection(nameof(Storage.ConnectionString)).Value;
            if (connectionString.IsNullOrEmpty())
                throw new NullReferenceException("Database ConnectionString can not be empty.");

            var assembly = Assembly.Load($"EventBus.Storage.{storageType}");
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var interfaceType = type.GetInterface(nameof(IStorageInitialization), true);
                if (interfaceType != null)
                {
                    var storageInitialization = (IStorageInitialization)Activator.CreateInstance(type);
                    if (storageInitialization != null) storageInitialization.Initialize(services, connectionString);
                }
            }

            return services;
        }
    }
}
