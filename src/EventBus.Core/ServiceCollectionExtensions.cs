using EventBus.Abstractions.IProviders;
using EventBus.Core.Providers;
using EventBus.Core.Services;
using EventBus.Storage.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventBus.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            PrintProjectName();

            services.AddHttpClient();
            services.AddStorage(configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<BufferQueueService>();
            services.AddSingleton<IBufferQueueService, BufferQueueService>();
            services.AddSingleton<IHostedService, BufferQueueService>();
            services.AddSingleton<ISubscriptionQueueProvider, SubscriptionQueueProvider>();

            services.AddScoped<IApplicationProvider, ApplicationProvider>();
            services.AddScoped<IEventProvider, EventProvider>();
            services.AddScoped<IEventRecordProvider, EventRecordProvider>();
            services.AddScoped<IRetryProvider, RetryProvider>();
            services.AddScoped<ISubscriptionProvider, SubscriptionProvider>();

            return services;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.InitializationDatabase();
            return applicationBuilder;
        }

        /// <summary>
        /// 打印项目名称
        /// </summary>
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
