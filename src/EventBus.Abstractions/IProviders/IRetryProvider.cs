using EventBus.Domain.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.IProviders
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
        Task<IRetryData> GetRetryAsync(string id);

        /// <summary>
        /// 获取事件的所有重试数据
        /// </summary>
        /// <param name="evnetId"></param>
        /// <returns></returns>
        Task<IRetryData[]> GetEventRetryAsync(string evnetId);
    }
}
