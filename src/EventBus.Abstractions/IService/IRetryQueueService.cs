using EventBus.Domain.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.IService
{
    public interface IRetryQueueService
    {
        Task PushAsync(IRetryData retryData);

        Task PushAsync(IRetryData[] retryDatas);
    }
}
