using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Extensions;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventLogProvider : BaseRepository<EventRecord>, IEventLogProvider
    {
        public EventLogProvider(IRepository repository) : base(repository)
        {
        }

        public async Task<IEventRecord> GetEventLogAsync(Guid eventLogId)
        {
            var evnetLog = await Get().FirstOrDefaultAsync(a => a.Id == eventLogId);
            if (evnetLog == null) return null;

            return evnetLog;
        }

        public async Task<IEventRecord[]> GetEventLogsAsync(Guid eventId)
        {
            var eventLogs = await Get().Where(a => a.EventId == eventId).ToArrayAsync();
            if (eventLogs.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return eventLogs;
        }

        public async Task<IEventRecord[]> GetEventLogsAsync(int start, int count, DateTime? begin, DateTime? end)
        {
            var query = Get();
            if (begin.HasValue && end.HasValue) query.Where(a => a.CreateTime > begin.Value && a.CreateTime < end.Value);

            var eventLogs = await Get().Skip(start).Take(count).ToArrayAsync();
            if (eventLogs.IsNullOrEmpty()) return EventRecord.EmptyArray;

            return eventLogs;
        }
    }
}
