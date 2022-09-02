using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 应用
    /// </summary>
    public class Application : BaseModel
    {
        /// <summary>
        /// 应用 Id
        /// </summary>
        public Guid ApplicationId { set; get; }

        /// <summary>
        /// 应用名
        /// </summary>
        public string ApplicationName { set; get; }

        /// <summary>
        /// 拥有的接入点
        /// </summary>
        public ApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
