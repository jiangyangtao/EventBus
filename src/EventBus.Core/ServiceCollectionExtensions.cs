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
            PrintProjectName();
            services.AddStorage(configuration);
            return services;
        }

        private static void PrintProjectName()
        {
            Console.WriteLine("");
            Console.WriteLine("     ______                        __     ____                 ");
            Console.WriteLine("    / ____/ _   __  ___    ____   / /_   / __ )  __  __   _____");
            Console.WriteLine(@"   / __/   | | / / / _ \  / __ \ / __/  / __  | / / / /  / ___/");
            Console.WriteLine("  / /___   | |/ / /  __/ / / / // /_   / /_/ / / /_/ /  (__  ) ");
            Console.WriteLine(@" /_____/   |___/  \___/ /_/ /_/ \__/  /_____/  \____/  /____/  ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
