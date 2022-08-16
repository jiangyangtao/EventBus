using EventBus.Extensions;
using EventBus.Storage.Abstractions;
using EventBus.Storage.Abstractions.Enums;
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
            var storageSection = configuration.GetSection("Storage");
            var storageType = storageSection.GetSection("StorageType").Value;

            var config = storageSection.GetSection<StorageConfig>();
            var r = Enum.TryParse(storageType, true, out StorageType databaseType);
            if (r == false)
            {
                databaseType = StorageType.Sqlite;
                logger.LogError($"Unrecognizable StorageType:{storageType}. Enable default database:Sqlite.");
                logger.LogError("Please choose one of the MySql / Sqlite / Sql Server / MongoDB.");
            }

            logger.LogInformation($"Database: {databaseType}");

            var connectionString = storageSection.GetSection("ConnectionString").Value;
            if (connectionString.IsNullOrEmpty())
                throw new NullReferenceException("Database ConnectionString can not be empty.");


            //var assembly = Assembly.GetEntryAssembly();
            var storageInitializationType = typeof(IStorageInitialization).Assembly;
            var types = storageInitializationType.GetTypes();
            foreach (var type in types)
            {
                if (type.GetInterfaces().Length > 0 && type.IsAbstract == false)
                {
                    var storageInitialization = (IStorageInitialization)Activator.CreateInstance(type);
                    if (storageInitialization.StorageType == databaseType) storageInitialization.Initialize(services);
                }
            }

            return services;
        }
    }
}
