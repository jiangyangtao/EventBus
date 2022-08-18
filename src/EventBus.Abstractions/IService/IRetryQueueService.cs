using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IService
{
    public interface IRetryQueueService
    {
        Task PushAsync(IRetryData retryData);

        Task PushAsync(IRetryData[] retryDatas);
    }
}
