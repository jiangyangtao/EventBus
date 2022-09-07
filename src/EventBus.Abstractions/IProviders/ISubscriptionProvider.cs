using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    public interface ISubscriptionProvider
    {
        Task<Guid> AddAsync(Guid eventId, Guid endpointId);

        Task RemoveAsync(Guid subscriptionId);

        Task<ISubscription> GetSubscriptionAsync(Guid subscriptionId);

        Task<ISubscription[]> GetSubscriptionsAsync(Guid eventId, string endpointName, int start, int count);

        Task<long> GetSubscriptionCountAsync(Guid eventId, string endpointName);

        Task<ISubscription[]> GetSubscriptionsAsync(Guid eventId);

        Task<ISubscriptionRecord[]> GetSubscriptionRecordsAsync(Guid eventRecordId);
    }
}
