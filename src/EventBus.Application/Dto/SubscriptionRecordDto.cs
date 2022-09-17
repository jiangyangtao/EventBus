using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBus.Application.Dto
{
    public class EventRecordSubscriptionDtoBase
    {
        public Guid EventRecordSubscriptionId { get; set; }
    }

    public class EventRecordSubscriptionDto : EventRecordSubscriptionDtoBase
    {
        public EventRecordSubscriptionDto(IEventRecordSubscription record)
        {
            EventRecordSubscriptionId = record.Id;
            EndpointName = record.EndpointName;
            EndpointUrl = record.EndpointUrl;
            SubscriptionProtocol = record.SubscriptionProtocol;
            RequestTimeout = record.RequestTimeout;
            FailedRetryPolicy = record.FailedRetryPolicy.Select(a=> new RetryPolicyDto(a)).ToArray();
            SubscriptionResult = record.SubscriptionResult;
        }

        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { get; }

        /// <summary>
        /// 订阅协议
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ProtocolType SubscriptionProtocol { get; }

        /// <summary>
        /// 请求超时时间，单位：秒
        /// </summary>
        public int RequestTimeout { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public RetryPolicyDto[] FailedRetryPolicy { get; }

        /// <summary>
        /// 订阅结果，true 成功，false 失败
        /// </summary>
        public bool SubscriptionResult { get; }
    }

    public class EventRecordSubscriptionResult : EventRecordSubscriptionDto
    {
        public EventRecordSubscriptionResult(IEventRecordSubscription record) : base(record)
        {

        }
    }
}
