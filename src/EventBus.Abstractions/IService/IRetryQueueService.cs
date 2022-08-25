using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IService
{
    public interface IRetryQueueService
    {
        Task PutAsync(IRetryData retryData);

        Task PutAsync(IRetryData[] retryDatas);
    }
}
