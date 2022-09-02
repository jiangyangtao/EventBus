

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public interface IBaseModel
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
