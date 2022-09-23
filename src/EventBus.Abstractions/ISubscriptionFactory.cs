using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;

namespace EventBus.Abstractions
{
    public interface ISubscriptionFactory
    {
        public IAsyncSubscriptionProvider CreateSubscriptionProvider(IEventRecordSubscription subscription);
    }
}
