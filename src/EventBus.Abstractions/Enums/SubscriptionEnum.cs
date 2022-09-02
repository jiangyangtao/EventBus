

namespace EventBus.Abstractions.Enums
{
    public enum RetryDelayUnit
    {
        /// <summary>
        /// 秒
        /// </summary>
        Second,

        /// <summary>
        /// 分
        /// </summary>
        Minute,

        /// <summary>
        /// 时
        /// </summary>
        Hour,

        /// <summary>
        /// 天
        /// </summary>
        Day
    }

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
