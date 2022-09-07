﻿using EventBus.Abstractions.IModels;

namespace EventBus.Application.Dto
{
    public class EventRecordDto
    {
        public EventRecordDto(IEventRecord record)
        {
            QueryString = record.QueryString;
            Header = record.Header;
            Data = record.Data;
            RecordTime = record.RecordTime;
            SubscriptionCompletionRate = record.SubscriptionCompletionRate;
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString { set; get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, string> Header { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { set; get; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { set; get; }

        /// <summary>
        /// 订阅完成率
        /// </summary>
        public decimal SubscriptionCompletionRate { set; get; }
    }

    public class EventRecordResult : EventRecordDto
    {
        public EventRecordResult(IEventRecord record) : base(record)
        {
        }
    }

    public class EventRecordQueryDto : PagingParameter
    {
        public DateTime? BeginTime { set; get; }

        public DateTime? EndTime { set; get; }

        internal bool Verification(out string message)
        {
            message = string.Empty;

            if (BeginTime.HasValue == false) return false;
            if (EndTime.HasValue == false) return false;

            if (BeginTime.Value > EndTime.Value)
            {
                message = "开始时间不能大于结束时间";
                return false;
            }

            return true;
        }
    }

    public class EventRecordPaginationResult : PaginationResult<EventRecordResult>
    {
        public EventRecordPaginationResult(long count, IEventRecord[] records) : base(count)
        {
            List = records.Select(a => new EventRecordResult(a)).ToArray();
        }
    }
}
