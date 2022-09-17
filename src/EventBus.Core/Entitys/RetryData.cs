using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{

    public class RetryData : BaseEntity<RetryData>, IRetryData
    {
        public RetryData()
        {
        }

        public RetryData(
            Guid eventId,
            Guid eventRecordId,
            Guid eventRecordSubscriptionId,
            RetryDelayUnit delayUnit,
            int delayCount)
        {
            var retryDelay = TimeSpan.FromSeconds(delayCount);
            if (delayUnit == RetryDelayUnit.Minute)
            {
                retryDelay = TimeSpan.FromMinutes(delayCount);
            }

            if (delayUnit == RetryDelayUnit.Hour)
            {
                retryDelay = TimeSpan.FromHours(delayCount);
            }

            if (delayUnit == RetryDelayUnit.Day)
            {
                retryDelay = TimeSpan.FromDays(delayCount);
            }


            RetryTime = DateTime.Now.Add(retryDelay);
            EventId = eventId;
            EventRecordId = eventRecordId;
            EventRecordSubscriptionId = eventRecordSubscriptionId;
        }

        /// <summary>
        /// 重试时间
        /// </summary>
        public DateTime RetryTime { set; get; }

        /// <summary>
        /// 订阅的分组日志 Id
        /// </summary>
        public Guid EventRecordSubscriptionId { set; get; }

        public Guid EventRecordId { set; get; }

        public Guid EventId { set; get; }


        [NotMapped]
        public IEvent Event { set; get; }

        [NotMapped]
        public IEventRecord EventRecord { set; get; }

        [NotMapped]
        public IEventRecordSubscription EventRecordSubscription { set; get; }
    }
}
