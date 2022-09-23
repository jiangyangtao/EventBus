using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Subscription
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventSubscription(this IServiceCollection services)
        {
            services.AddSingleton<ISubscriptionFactory, SubscriptionFactory>();
            return services;
        }
    }
}
