using EventBus.Abstractions.IModels;

namespace EventBus.Application.Dto
{
    public class RetryDataDtoBase
    {
        public Guid RetryDataId { set; get; }
    }

    public class RetryDataDto : RetryDataDtoBase
    {
        public RetryDataDto(IRetryData retryData)
        {
            RetryDataId = retryData.Id;
            RetryTime = retryData.RetryTime;

            if (retryData.Event != null) EventName = retryData.Event.EventName;
            if (retryData.EventRecord != null) EventRecord = new EventRecordDto(retryData.EventRecord);
            if (retryData.SubscriptionRecord != null) SubscriptionRecord = new SubscriptionRecordDto(retryData.SubscriptionRecord);
        }

        public string EventName { set; get; }

        public EventRecordDto EventRecord { set; get; }

        public SubscriptionRecordDto SubscriptionRecord { set; get; }


        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { get; }
    }

    public class RetryDataQueryDto : PagingParameter
    {
        public string EventName { set; get; }

        public string EndpointName { set; get; }
    }

    public class RetryDataResult : RetryDataDto
    {
        public RetryDataResult(IRetryData retryData) : base(retryData)
        {
        }
    }

    public class RetryDataPaginationResult : PaginationResult<RetryDataResult>
    {
        public RetryDataPaginationResult(long count, IRetryData[] retryDatas) : base(count)
        {
            List = retryDatas.Select(a => new RetryDataResult(a)).ToArray();
        }
    }
}
