using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class EndpointSubscriptionRecordDtoBase
    {
        [Required]
        public Guid EndpointSubscriptionRecordId { set; get; }
    }

    public class EndpointSubscriptionRecordDto : EndpointSubscriptionRecordDtoBase
    {
        public EndpointSubscriptionRecordDto(IEndpointSubscriptionRecord endpointRecord)
        {
            EndpointSubscriptionRecordId = endpointRecord.Id;
            SubscriptionType = endpointRecord.SubscriptionType;
            SubscriptionTime = endpointRecord.SubscriptionTime;
            ResponseTime = endpointRecord.ResponseTime;
            IsSuccessStatusCode = endpointRecord.IsSuccessStatusCode;
            ResponseStatucCode = endpointRecord.ResponseStatucCode;
            ResponseContent = endpointRecord.ResponseContent;
            UsageTime = endpointRecord.UsageTime;
        }

        /// <summary>
        /// 通知类型
        /// </summary>
        public SubscriptionType SubscriptionType { get; }

        /// <summary>
        /// 通知时间
        /// </summary>
        public DateTime SubscriptionTime { get; }

        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime ResponseTime { get; }

        /// <summary>
        /// 是否成功状态码
        /// </summary>
        public bool IsSuccessStatusCode { get; }

        /// <summary>
        /// 响应的状态码
        /// </summary>
        public string ResponseStatucCode { get; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseContent { get; }

        /// <summary>
        /// 耗时，单位：秒
        /// </summary>
        public long UsageTime { get; }
    }

    public class EndpointSubscriptionRecordResult : EndpointSubscriptionRecordDto
    {
        public EndpointSubscriptionRecordResult(IEndpointSubscriptionRecord endpointRecord) : base(endpointRecord)
        {
        }
    }
}
