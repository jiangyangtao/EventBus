using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Text;

namespace EventBus.Core.Entitys
{
    public class EventRecord : BaseEntity<EventRecord>, IEventRecord
    {
        public EventRecord() { }

        public EventRecord(Guid eventId, HttpRequest request)
        {
            EventId = eventId;
            QueryString = request.QueryString.ToString();

            var streamReader = new StreamReader(request.Body);
            Data = streamReader.ReadToEnd();

            Header = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString());
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

        public string Data { set; get; }

        public DateTime RecordTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public Guid EventId { set; get; }

        [NotMapped]
        public IEvent Event { set; get; }

        [NotMapped]
        public ISubscriptionRecord[] ISubscriptionRecords { set; get; }

        public HttpContent BuilderHttpContent()
        {
            return new StringContent(Data, Encoding.UTF8, "application/json");
        }
    }
}
