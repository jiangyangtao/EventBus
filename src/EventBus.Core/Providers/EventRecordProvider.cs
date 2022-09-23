using EventBus.Abstractions.Enums;
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

            var eventRecord = await BuildEventRecordAsync(e.Id);
            await CreateAsync(eventRecord);

            if (e.Subscriptions.NotNullAndEmpty())
            {
                var records = e.Subscriptions.Select(subscription => new EventRecordSubscription(e.Id, eventRecord, subscription)).ToArray();

                if (records.NotNullAndEmpty())
                {
                    await AddRangeAsync(records);
                    await _subscriptionQueueProvider.PutAsync(records);
                }
            }
        }

        public async Task PublishAsync(Guid eventId, IEventRecord record)
        {
            var e = await _eventProvider.GetEventAsync(eventId);
            if (e == null) return;
            if (e.Subscriptions.IsNullOrEmpty()) return;

            var eventRecord = BuildEventRecord(e.Id, record);
            await CreateAsync(eventRecord);

            if (e.Subscriptions.NotNullAndEmpty())
            {
                var records = e.Subscriptions.Select(subscription => new EventRecordSubscription(e.Id, eventRecord, subscription)).ToArray();

                if (records.NotNullAndEmpty())
                {
                    await AddRangeAsync(records);
                    await _subscriptionQueueProvider.PutAsync(records);
                }
            }
        }

        private async Task<EventRecord> BuildEventRecordAsync(Guid eventId)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var QueryString = request.QueryString.ToString();
            var streamReader = new StreamReader(request.Body);
            var data = await streamReader.ReadToEndAsync();
            var header = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString());
            var ipaddress = _httpContextAccessor.HttpContext.GetClientIPAddress();

            return new EventRecord
            {
                EventId = eventId,
                QueryString = request.QueryString.ToString(),
                Data = data,
                Header = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString()),
                RecordTime = DateTime.Now,
                ClientIPAddress = ipaddress.ToString(),
            };
        }

        private EventRecord BuildEventRecord(Guid eventId, IEventRecord record)
        {
            return new EventRecord
            {
                EventId = eventId,
                QueryString = record.QueryString,
                Data = record.Data,
                Header = record.Header,
                RecordTime = DateTime.Now,
                ClientIPAddress = record.ClientIPAddress,
            };
        }

        public async Task SubscriptionAsync(Guid subscriptionId)
        {
            var eventRecordSubscription = await GetByIdAsync<EventRecordSubscription>(subscriptionId);
            if (eventRecordSubscription == null) return;
            if (eventRecordSubscription.SubscriptionResult) return;

            eventRecordSubscription.FailToRetry = false;
            eventRecordSubscription.SubscriptionType = SubscriptionType.Manual;

            var evnetRecord = await GetEventRecordAsync(eventRecordSubscription.EventRecordId);
            if (evnetRecord == null) return;

            eventRecordSubscription.EventRecord = evnetRecord;
            await _subscriptionQueueProvider.PutAsync(eventRecordSubscription);
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
            if (begin.HasValue && end.HasValue) query = query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            var evnetRecords = await query.Skip(start).Take(count).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return EventRecord.EmptyArray;

            var eventIds = evnetRecords.Select(a => a.EventId).Distinct().ToArray();
            var events = await Get<Event>(a => eventIds.Contains(a.Id)).ToArrayAsync();
            if (events.NotNullAndEmpty())
            {
                foreach (var eventRecord in evnetRecords)
                {
                    eventRecord.Event = events.FirstOrDefault(a => a.Id == eventRecord.EventId);
                }
            }

            return evnetRecords;
        }

        public async Task<long> GetEventRecordCountAsync(DateTime? begin, DateTime? end)
        {
            var query = Get();
            if (begin.HasValue && end.HasValue) query = query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            return await query.CountAsync();
        }

        public async Task<IEventRecordSubscription[]> GetEventRecordSubscriptionsAsync(Guid eventRecordId)
        {
            var records = await Get<EventRecordSubscription>(a => a.EventRecordId == eventRecordId).ToArrayAsync();
            if (records.IsNullOrEmpty()) return EventRecordSubscription.EmptyArray;

            return records;
        }

        public async Task<IEndpointSubscriptionRecord[]> GetEndpointSubscriptionRecordsAsync(Guid subscriptionRecordId)
        {
            var records = await Get<EndpointSubscriptionRecord>(a => a.EventRecordSubscriptionId == subscriptionRecordId).ToArrayAsync();
            if (records.IsNullOrEmpty()) return EndpointSubscriptionRecord.EmptyArray;

            return records;
        }

        public async Task<IEventRecordSubscription> GetEventRecordSubscriptionAsync(Guid eventRecordSubscriptionId)
        {
            return await GetByIdAsync<EventRecordSubscription>(eventRecordSubscriptionId);
        }
    }
}
