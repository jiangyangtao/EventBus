using EventBus.Abstractions.IProviders;
using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Core.Base;

namespace EventBus.Core.Providers
{
    internal class EventManager : BaseRepository, IEventManager
    {
        private readonly IEventProvider _eventProvider;

        public EventManager(IRepository repository, IEventProvider eventProvider) : base(repository)
        {
            _eventProvider = eventProvider;
        }

        public Task AddOrUpdateAsync(IEvent data)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAsync(string subscriptionGroupLogId)
        {
            throw new NotImplementedException();
        }

        public Task PublishAsync(string eventId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEvent data)
        {
            throw new NotImplementedException();
        }
    }
}
