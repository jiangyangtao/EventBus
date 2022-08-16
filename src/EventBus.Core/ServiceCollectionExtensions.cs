using EventBus.Storage.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, ILogger logger)
        {
            services.AddStorage(configuration, logger);
            return services;
        }
    }
}
