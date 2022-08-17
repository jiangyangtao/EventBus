using Colorful;
using EventBus.Storage.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace EventBus.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStorage(configuration);
            Colorful.Console.WriteAscii("EventBus", FigletFont.Default, Color.White);
            return services;
        }
    }
}
