using EventBus.Abstractions.Enums;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 应用接入点
    /// </summary>
    public interface IApplicationEndpoint : IBaseModel
    {
        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { get; }

        /// <summary>
        /// 应用 Id
        /// </summary>
        public Guid ApplicationId { get; }

        /// <summary>
        /// 通知协议
        /// </summary>
        public ProtocolType NoticeProtocol { get; }

        /// <summary>
        /// 所属应用
        /// </summary>
        public IApplication Application { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { set; get; }

        /// <summary>
        /// 获取重试策略
        /// </summary>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public IRetryPolicy GetRetryPolicy(int retryCount = 1);
    }
}
