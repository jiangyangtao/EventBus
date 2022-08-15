using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Enums
{
    /// <summary>
    /// 协议类型
    /// </summary>
    public enum ProtocolType
    {
        Http,
        gRPC,
    }

    public enum NoticeType
    {
        /// <summary>
        /// 自动
        /// </summary>
        Automatic,

        /// <summary>
        /// 手动
        /// </summary>
        Manual
    }
}
