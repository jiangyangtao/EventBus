using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var storageSection = configuration.GetSection("Storage");
            var storageType = storageSection.GetSection("StorageType").Value;
            var connectionString = storageSection.GetSection("ConnectionString").Value;

            return services;
        }
    }
}
