using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventBus.Application.Dto
{
    public class SubscriptionRecordDtoBase
    {
        public Guid SubscriptionRecordId { get; set; }
    }

    public class SubscriptionRecordDto : SubscriptionRecordDtoBase
    {
        public SubscriptionRecordDto(ISubscriptionRecord record)
        {
            SubscriptionRecordId = record.Id;
            EndpointName = record.EndpointName;
            EndpointUrl = record.EndpointUrl;
            SubscriptionProtocol = record.SubscriptionProtocol;
            RequestTimeout = record.RequestTimeout;
            FailedRetryPolicy = record.FailedRetryPolicy;
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
        public IRetryPolicy[] FailedRetryPolicy { get; }

        /// <summary>
        /// 订阅结果，true 成功，false 失败
        /// </summary>
        public bool SubscriptionResult { get; }
    }

    public class SubscriptionRecordResult : SubscriptionRecordDto
    {
        public SubscriptionRecordResult(ISubscriptionRecord record) : base(record)
        {

        }
    }
}
