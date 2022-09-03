

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
        public IApplicationEndpoint[] ApplicationEndpoints {  get; }
    }
}
