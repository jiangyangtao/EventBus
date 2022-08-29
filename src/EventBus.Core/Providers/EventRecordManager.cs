using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;

namespace EventBus.Core.Providers
{
    internal class EventRecordManager : BaseRepository<EventRecord>, IEventRecordManager
    {
        private readonly IEventProvider _eventProvider;
        private readonly ISubscriptionQueueProvider _subscriptionQueueProvider;

        public EventRecordManager(
            IRepository repository,
            IEventProvider eventProvider,
            ISubscriptionQueueProvider subscriptionQueueProvider) : base(repository)
        {
            _eventProvider = eventProvider;
            _subscriptionQueueProvider = subscriptionQueueProvider;
        }

        public async Task PublishAsync(IEventRecord eventRecord)
        {
            var e = await _eventProvider.GetEventAsync(eventRecord.Event.Id);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;

            await CreateAsync(new EventRecord(eventRecord));

            if (e.Subscriptions.NotNullAndEmpty())
            {
                var records = e.Subscriptions.Where(subscription => subscription.ApplicationEndpoint != null)
                    .Select(subscription => new SubscriptionRecord(eventRecord, subscription.ApplicationEndpoint)).ToArray();

                if (records.NotNullAndEmpty())
                {
                    await AddRangeAsync(records);
                    await _subscriptionQueueProvider.PutAsync(records);
                }
            }
        }
    }
}
