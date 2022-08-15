using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Domain.Enums
{
    public enum RetryBehavior
    {
        /// <summary>
        /// 重试
        /// </summary>
        Retry,

        /// <summary>
        /// 丢弃
        /// </summary>
        Discard,

        /// <summary>
        /// 通知相关人员
        /// </summary>
        Notice,
    }
}
