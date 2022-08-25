using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventRecordProvider
    {       
        Task<IEventRecord> GetEventRecordAsync(Guid eventRecordId);

        Task<IEventRecord[]> GetEventRecordsAsync(Guid eventId);

        Task<IEventRecord[]> GetEventRecordsAsync(int start, int count, DateTime? begin, DateTime? end);
    }
}
