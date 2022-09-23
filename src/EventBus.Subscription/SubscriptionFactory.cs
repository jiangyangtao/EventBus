using EventBus.Abstractions;
using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Subscription.Providers;

namespace EventBus.Subscription
{
    internal class SubscriptionFactory : ISubscriptionFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SubscriptionFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IAsyncSubscriptionProvider CreateSubscriptionProvider(IEventRecordSubscription subscription)
        {
            if (subscription.EventRecord == null) throw new NullReferenceException("subscription.EventRecord");

            if (subscription.SubscriptionProtocol == ProtocolType.Http) return new HttpSubscriptionProvider(_serviceProvider, subscription);

            return new GrpcSubscriptionProvider(_serviceProvider, subscription);
        }
    }
}
