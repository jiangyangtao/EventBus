﻿using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    public interface IRetryProvider
    {
        /// <summary>
        /// 重试
        /// </summary>
        /// <returns></returns>
        Task RetryAsync();

        /// <summary>
        /// 重试一条数据
        /// </summary>
        /// <param name="retryDataId"></param>
        /// <returns></returns>
        Task RetryAsync(Guid retryDataId);

        /// <summary>
        /// 获取重试数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RetryData> GetRetryAsync(Guid id);

        /// <summary>
        /// 获取事件的所有重试数据
        /// </summary>
        /// <param name="evnetId"></param>
        /// <returns></returns>
        Task<RetryData[]> GetEventRetryAsync(Guid evnetId);

        /// <summary>
        /// 获取重试次数
        /// </summary>
        /// <param name="subscriptionRecordId"></param>
        /// <returns></returns>
        Task<int> GetRetryCountAsync(Guid subscriptionRecordId);
    }
}
