using EventBus.Domain.IModels;
using EventBus.Domain.IProviders;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventProvider : BaseService, IEventProvider
    {
        public EventProvider(IRepository repository) : base(repository)
        {
        }

        public Task<IEvent> GetEventAsync(string eventId)
        {
            throw new NotImplementedException();
        }

        public Task<IEvent[]> GetEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEvent[]> GetEventsAsync(int start, int count, string enentName)
        {
            throw new NotImplementedException();
        }
    }
}
