using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventRecordProvider : BaseRepository<EventRecord>, IEventRecordProvider
    {
        public EventRecordProvider(IRepository repository) : base(repository)
        {
        }

        public async Task<IEventRecord> GetEventRecordAsync(Guid eventRecordId)
        {
            var evnetRecord = await Get().FirstOrDefaultAsync(a => a.Id == eventRecordId);
            if (evnetRecord == null) return null;

            return evnetRecord;
        }

        public async Task<IEventRecord[]> GetEventRecordsAsync(Guid eventId)
        {
            var evnetRecords = await Get().Where(a => a.EventId == eventId).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return evnetRecords;
        }

        public async Task<IEventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end)
        {
            var query = Get();
            if (begin.HasValue && end.HasValue) query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            var evnetRecords = await Get().Skip(start).Take(count).ToArrayAsync();
            if (evnetRecords.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return evnetRecords;
        }
    }
}
