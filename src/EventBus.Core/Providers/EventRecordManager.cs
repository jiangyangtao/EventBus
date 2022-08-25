using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Providers
{
    internal class EventRecordManager : BaseRepository<EventRecord>, IEventRecordManager
    {
        private IEventProvider _eventProvider;

        public EventRecordManager(IRepository repository, IEventProvider eventProvider) : base(repository)
        {
            _eventProvider = eventProvider;
        }

        public async Task PublishAsync(IEventRecord eventRecord)
        {
            var e = await _eventProvider.GetEventAsync(eventRecord.Event.Id);
            if (e == null) return;

            // TODO 
        }
    }
}
