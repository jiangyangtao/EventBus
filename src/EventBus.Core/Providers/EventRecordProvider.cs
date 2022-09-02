using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventRecordProvider : BaseRepository<Entitys.EventRecord>, IEventRecordProvider
    {
        private readonly IEventProvider _eventProvider;
        private readonly ISubscriptionQueueProvider _subscriptionQueueProvider;

        public EventRecordProvider(IRepository repository,
            IEventProvider eventProvider,
            ISubscriptionQueueProvider subscriptionQueueProvider) : base(repository)
        {
            _eventProvider = eventProvider;
            _subscriptionQueueProvider = subscriptionQueueProvider;
        }

        public async Task PublishAsync(Abstractions.IModels.EventRecord eventRecord)
        {
            var e = await _eventProvider.GetEventAsync(eventRecord.Event.Id);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;

            await CreateAsync(new Entitys.EventRecord(e.Id, eventRecord));

            if (e.Subscriptions.NotNullAndEmpty())
            {
                var records = e.Subscriptions.Where(subscription => subscription.ApplicationEndpoint != null)
                    .Select(subscription => new SubscriptionRecord(e.Id, eventRecord, subscription.ApplicationEndpoint)).ToArray();

                if (records.NotNullAndEmpty())
                {
                    await AddRangeAsync(records);
                    await _subscriptionQueueProvider.PutAsync(records);
                }
            }
        }

        public async Task<Abstractions.IModels.EventRecord> GetEventRecordAsync(Guid eventRecordId)
        {
            var evnetRecord = await GetByIdAsync(eventRecordId);
            if (evnetRecord == null) return null;

            return evnetRecord;
        }

        public async Task<Abstractions.IModels.EventRecord[]> GetEventRecordsAsync(Guid eventId)
        {
            var evnetRecords = await Get(a => a.EventId == eventId).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return Entitys.EventRecord.EmptyArray;

            return evnetRecords;
        }

        public async Task<Abstractions.IModels.EventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end)
        {
            var query = Get();
            if (begin.HasValue && end.HasValue) query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            var evnetRecords = await query.Skip(start).Take(count).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return Entitys.EventRecord.EmptyArray;

            return evnetRecords;
        }
    }
}
