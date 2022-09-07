using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class SubscriptionDtoBase
    {
        [Required]
        public Guid SubscriptionId { get; set; }
    }

    public class SubscriptionAddDto
    {
        public Guid EventId { set; get; }

        public Guid ApplicationEndpointId { set; get; }
    }

    public class SubscriptionQueryDto : PagingParameter
    {
        [Required]
        public Guid EventId { set; get; }

        public string EndpointName { set; get; }
    }

    public class SubscriptionResult : SubscriptionDtoBase
    {
        public SubscriptionResult(ISubscription subscription)
        {
            SubscriptionId = subscription.Id;
            EndpointName = subscription.EndpointName;
            EndpointUrl = subscription.EndpointUrl;
            SubscriptionProtocol = subscription.SubscriptionProtocol;
            RequestTimeout = subscription.RequestTimeout;
            FailedRetryPolicy = subscription.FailedRetryPolicy;
        }

        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { set; get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { set; get; }

        /// <summary>
        /// 订阅协议
        /// </summary>
        public ProtocolType SubscriptionProtocol { set; get; }

        /// <summary>
        /// 请求超时时间
        /// </summary>
        public int RequestTimeout { set; get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { set; get; }
    }

    public class SubscriptionPaginationResult : PaginationResult<SubscriptionResult>
    {
        public SubscriptionPaginationResult(long count, ISubscription[] subscriptions) : base(count)
        {
            List = subscriptions.Select(a => new SubscriptionResult(a)).ToArray();
        }
    }
}
