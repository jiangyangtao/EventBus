using EventBus.Abstractions.IProviders;
using EventBus.Domain.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventManager : BaseService, IEventManager
    {
        public EventManager(IRepository repository) : base(repository)
        {
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
