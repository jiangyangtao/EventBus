using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventBus.Core.Entitys
{
    internal class EventRecord : BaseEntity<EventRecord>, Abstractions.IModels.EventRecord
    {
        public EventRecord() { }

        public EventRecord(Guid eventId, Abstractions.IModels.EventRecord eventRecord)
        {
            EventId = eventId;

            QueryString = eventRecord.QueryString;
            Data = eventRecord.Data;
            Header = eventRecord.Header;
            RecordTime = DateTime.Now;
        }

        public string QueryString { set; get; }

        [NotMapped]
        public IDictionary<string, string> Header
        {
            set
            {
                if (value.NotNullAndEmpty()) HeaderString = JsonConvert.SerializeObject(value, Formatting.Indented);
                else HeaderString = string.Empty;
            }
            get
            {
                if (HeaderString.IsNullOrEmpty()) return null;

                return JsonConvert.DeserializeObject<IDictionary<string, string>>(HeaderString);
            }
        }

        public string HeaderString { set; get; }

        [NotMapped]
        public object Data
        {
            set
            {
                if (value != null) DataString = JsonConvert.SerializeObject(value, Formatting.Indented);
                else DataString = string.Empty;
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
        public Abstractions.IModels.Event Event { set; get; }

        [NotMapped]
        public Abstractions.IModels.SubscriptionRecord[] ISubscriptionRecords { set; get; }
    }
}
