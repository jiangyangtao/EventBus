using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventBus.Core.Entitys
{
    public class EventRecord : BaseEntity<EventRecord>, IEventRecord
    {

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

        public string Data { set; get; }

        public string ClientIPAddress { set; get; }

        public DateTime RecordTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public Guid EventId { set; get; }

        [NotMapped]
        public IEvent Event { set; get; }

        [NotMapped]
        public IEventRecordSubscription[] EventRecordSubscriptions { set; get; }

        public HttpContent BuilderHttpContent()
        {
            return new StringContent(Data, Encoding.UTF8, "application/json");
        }
    }
}
