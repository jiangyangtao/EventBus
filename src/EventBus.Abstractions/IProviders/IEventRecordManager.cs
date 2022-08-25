using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IEventRecordManager
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="eventRecord"></param>
        /// <returns></returns>
        Task PublishAsync(IEventRecord eventRecord);
    }
}
