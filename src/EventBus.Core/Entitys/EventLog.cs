using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class EventLog : BaseEntity<EventLog>, IEventLog
    {
        public string QueryString { set; get; }

        public Dictionary<string, object> Header { set; get; }

        public object Data { set; get; }

        public DateTime OccurrenceTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public Guid EventId { set; get; }

        public IEvent Event { set; get; }

        public ISubscriptionGroupLog[] ISubscriptionGroupLogs { set; get; }
    }
}
