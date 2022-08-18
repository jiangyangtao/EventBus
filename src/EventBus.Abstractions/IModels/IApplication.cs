using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 应用
    /// </summary>
    public interface IApplication : IBaseModel
    {
        /// <summary>
        /// 应用名
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// 拥有的接入点
        /// </summary>
        public IApplicationEndpoint[] ApplicationEndpoints { get; }
    }
}
