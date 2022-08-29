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

        public EventRecord(Guid eventId, IEventRecord eventRecord)
        {
            EventId = eventId;

            QueryString = eventRecord.QueryString;
            Data = eventRecord.Data;
            Header = eventRecord.Header;
            RecordTime = DateTime.Now;
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

        public DateTime RecordTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public Guid EventId { set; get; }

        [NotMapped]
        public IEvent Event { set; get; }

        [NotMapped]
        public ISubscriptionRecord[] ISubscriptionRecords { set; get; }

        public HttpContent BuilderHttpContent()
        {
            // TODO 实现一个 HttpContent
            throw new NotImplementedException();
        }
    }
}
