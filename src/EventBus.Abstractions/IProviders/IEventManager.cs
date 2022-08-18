using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventManager
    {
        Task AddOrUpdateAsync(IEvent data);

        Task RemoveAsync(IEvent data);

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        Task PublishAsync(string eventId);

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="subscriptionGroupLogId"></param>
        /// <returns></returns>
        Task NotifyAsync(string subscriptionGroupLogId);
    }
}
