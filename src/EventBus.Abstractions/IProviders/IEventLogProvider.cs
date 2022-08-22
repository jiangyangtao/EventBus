﻿using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventLogProvider
    {
        Task<IEventLog> GetEventLogAsync(Guid eventLogId);

        Task<IEventLog[]> GetEventLogsAsync(Guid eventId);

        Task<IEventLog[]> GetEventLogsAsync(int start, int count, DateTime? begin, DateTime? end);
    }
}
