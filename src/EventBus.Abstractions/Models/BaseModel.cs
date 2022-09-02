

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public abstract class BaseModel
    {     
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; }
    }
}
