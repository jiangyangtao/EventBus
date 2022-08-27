using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class EventRecord : BaseEntity<EventRecord>, IEventRecord
    {
        public EventRecord() { }

        public EventRecord(IEventRecord eventRecord)
        {
            QueryString = eventRecord.QueryString;

        }

        public string QueryString { set; get; }

        [NotMapped]
        public IDictionary<string, object> Header
        {
            set
            {
                if (value.NotNullAndEmpty()) HeaderString = JsonConvert.SerializeObject(value, Formatting.Indented);
            }
            get
            {
                if (HeaderString.IsNullOrEmpty()) return null;

                return JsonConvert.DeserializeObject<IDictionary<string, object>>(HeaderString);
            }
        }

        public string HeaderString { set; get; }

        [NotMapped]
        public object Data
        {
            set
            {
                if (value != null) DataString = JsonConvert.SerializeObject(value, Formatting.Indented);
            }

            get
            {
                if (DataString.IsNullOrEmpty()) return null;

                return JsonConvert.DeserializeObject(DataString);
            }
        }

        public string DataString { set; get; }

        public DateTime OccurrenceTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public Guid EventId { set; get; }

        public IEvent Event { set; get; }

        public ISubscriptionRecord[] ISubscriptionRecords { set; get; }

        public HttpContent BuilderHttpContent()
        {
            // TODO 实现一个 HttpContent
            throw new NotImplementedException();
        }
    }
}
