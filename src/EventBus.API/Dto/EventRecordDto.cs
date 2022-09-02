using EventBus.Abstractions.IModels;

namespace EventBus.API.Dto
{
    public class EventRecordDto : IEventRecord
    {
        public EventRecordDto(Guid eventId, object data, HttpRequest request)
        {
            Event = new EventDto(eventId);
            QueryString = request.QueryString.ToString();
            Header = request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString());
            Data = data;
            RecordTime = DateTime.Now;
        }

        public string QueryString { set; get; }

        public IDictionary<string, string> Header { set; get; }

        public object Data { set; get; }

        public DateTime RecordTime { set; get; }

        public decimal SubscriptionCompletionRate { set; get; }

        public IEvent Event { set; get; }

        public ISubscriptionRecord[] ISubscriptionRecords { set; get; }

        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public HttpContent BuilderHttpContent()
        {
            throw new NotImplementedException();
        }
    }
}
