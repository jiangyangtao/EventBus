using EventBus.Domain.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class EventLog : BaseEntity, IEventLog
    {
        public string QueryString { set; get; }

        public Dictionary<string, object> Header { set; get; }

        public object Data { set; get; }

        public DateTime OccurrenceTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public IEvent Event { set; get; }

        public ISubscriptionGroupLog[] ISubscriptionGroupLogs { set; get; }
    }
}
