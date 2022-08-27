using EventBus.Abstractions.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
        public IRetryPolicy[] FailedRetryPolicy { get; }
    }
}
