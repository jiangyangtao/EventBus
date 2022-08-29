

namespace EventBus.Abstractions.Enums
{
    public enum RetryDelayUnit
    {
        Second,
        Minute,
        Hour,
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
