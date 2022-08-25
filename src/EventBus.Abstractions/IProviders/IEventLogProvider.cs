using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventLogProvider
    {
        Task<IEventRecord> GetEventLogAsync(Guid eventLogId);

        Task<IEventRecord[]> GetEventLogsAsync(Guid eventId);

        Task<IEventRecord[]> GetEventLogsAsync(int start, int count, DateTime? begin, DateTime? end);
    }
}
