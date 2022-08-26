using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Core.Base;
using EventBus.Core.Entitys;
using EventBus.Storage.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EventBus.Core.Providers
{
    internal class EventManager : BaseRepository<Event>, IEventManager
    {
        private readonly IEventProvider _eventProvider;

        public EventManager(IRepository repository, IEventProvider eventProvider) : base(repository)
        {
            _eventProvider = eventProvider;
        }

        public async Task AddOrUpdateAsync(IEvent data)
        {
            var e = await Get(a => a.EventName == data.EventName).FirstOrDefaultAsync();
            if (e == null)
            {
                await CreateAsync(new Event(data));
                return;
            }

            e.EventName = e.EventName;
            e.EnableIPAddressWhiteList = e.EnableIPAddressWhiteList;
            e.IPAddressWhiteList = e.IPAddressWhiteList;
            e.EventProtocol = e.EventProtocol;
            await UpdateAsync(e);
        }

        public Task NotifyAsync(string subscriptionGroupLogId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAsync(IEvent data)
        {
            var e = await _eventProvider.GetEventAsync(data.EventName);
            if (e != null)
            {
                await DeleteAsync(a => a.Id == e.Id);
            }
        }
    }
}
