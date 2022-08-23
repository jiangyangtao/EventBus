using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    public interface IRetryProvider
    {
        /// <summary>
        /// 获取要执行的重试数据
        /// </summary>
        /// <returns></returns>
        Task<IRetryData[]> GetToBeExecutedRetryAsync();

        /// <summary>
        /// 获取重试数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IRetryData> GetRetryAsync(Guid id);

        /// <summary>
        /// 获取事件的所有重试数据
        /// </summary>
        /// <param name="evnetId"></param>
        /// <returns></returns>
        Task<IRetryData[]> GetEventRetryAsync(Guid evnetId);
    }
}
