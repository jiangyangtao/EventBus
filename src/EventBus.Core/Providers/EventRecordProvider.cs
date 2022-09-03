using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventRecordProvider : BaseRepository<EventRecord>, IEventRecordProvider
    {
        private readonly IEventProvider _eventProvider;
        private readonly ISubscriptionQueueProvider _subscriptionQueueProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventRecordProvider(IRepository repository,
            IEventProvider eventProvider,
            ISubscriptionQueueProvider subscriptionQueueProvider,
            IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _eventProvider = eventProvider;
            _subscriptionQueueProvider = subscriptionQueueProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task PublishAsync(Guid eventId)
        {
            var e = await _eventProvider.GetEventAsync(eventId);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;

            var request = _httpContextAccessor.HttpContext.Request;
            var eventRecord = new EventRecord(e.Id, request);
            await CreateAsync(eventRecord);

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

        public async Task<IEventRecord> GetEventRecordAsync(Guid eventRecordId)
        {
            var evnetRecord = await GetByIdAsync(eventRecordId);
            if (evnetRecord == null) return null;

            return evnetRecord;
        }

        public async Task<IEventRecord[]> GetEventRecordsAsync(Guid eventId)
        {
            var evnetRecords = await Get(a => a.EventId == eventId).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return evnetRecords;
        }

        public async Task<IEventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end)
        {
            var query = Get();
            if (begin.HasValue && end.HasValue) query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            var evnetRecords = await query.Skip(start).Take(count).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return evnetRecords;
        }
    }
}
