using EventBus.Extensions;
using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var log = serviceProvider.GetRequiredService<ILogger>();

            var storageSection = configuration.GetSection("Storage");
            var storageType = storageSection.GetSection("StorageType").Value;
            var r = Enum.TryParse(storageType, true, out StorageType databaseType);
            if (r == false)
            {
                databaseType = StorageType.Sqlite;
                log.LogError($"Unrecognizable StorageType:{storageType}. Enable default database:Sqlite.");
                log.LogError("Please choose one of the MySql / Sqlite / Sql Server / MongoDB.");
            }

            log.LogInformation($"Database: {databaseType}");

            var connectionString = storageSection.GetSection("ConnectionString").Value;
            if (connectionString.IsNullOrEmpty())
                throw new NullReferenceException("Database ConnectionString can not be empty.");

            if (databaseType == StorageType.Sqlite)
            {
                //services.TryAddEnumerable
            }

            if (databaseType == StorageType.MySql)
            {
                
            }

            if (databaseType == StorageType.SqlServer)
            {

            }

            if (databaseType == StorageType.MongoDB)
            {

            }

            return services;
        }
    }
}
