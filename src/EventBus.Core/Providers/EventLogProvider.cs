using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventLogProvider : IEventLogProvider
    {
        public Task<IEventLog> GetEventLogAsync(string eventLogId)
        {
            throw new NotImplementedException();
        }

        public Task<IEventLog[]> GetEventLogsAsync(string eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IEventLog[]> GetEventLogsAsync(int start, int count, DateTime? begin, DateTime? end)
        {
            throw new NotImplementedException();
        }
    }
}
