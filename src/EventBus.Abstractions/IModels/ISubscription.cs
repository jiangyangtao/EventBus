using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 订阅
    /// </summary>
    public interface ISubscription : IBaseModel
    {
        /// <summary>
        /// 事件 Id
        /// </summary>
        public Guid EvnetId { get;  }


        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get;  }


        /// <summary>
        /// 应用订阅的接入点 Id
        /// </summary>
        public Guid ApplicationEndpointId { set; }


        /// <summary>
        /// 应用订阅的接入点
        /// </summary>
        public IApplicationEndpoint ApplicationEndpoint { get; }
    }
}
