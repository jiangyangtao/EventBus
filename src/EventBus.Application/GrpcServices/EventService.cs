using EventBus.Abstractions.IProviders;
using EventBus.Application.Dto;
using EventBus.Extensions;
using EventBus.GrpcService.Protos;
using Grpc.Core;

namespace EventBus.Application.GrpcServices
{
    public class EventService : EventProto.EventProtoBase
    {
        private readonly IEventRecordProvider _eventRecordProvider;

        public EventService(IEventRecordProvider eventRecordProvider)
        {
            _eventRecordProvider = eventRecordProvider;
        }

        public override async Task<PushResponse> Push(PushModel request, ServerCallContext context)
        {
            if (request.EventId.IsNullOrEmpty()) return new PushResponse { Result = false };

            var parseResult = Guid.TryParse(request.EventId, out var eventId);
            if (parseResult == false) return new PushResponse { Result = false };

            var eventRecord = BuildEventRecord(request, context);
            await _eventRecordProvider.PublishAsync(eventId, eventRecord);
            return await base.Push(request, context);
        }

        private static EventRecordDataDto BuildEventRecord(PushModel param, ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            var header = httpContext.Request.Headers.ToDictionary(a => a.Key, a => a.Value.ToString());
            var ipaddress = httpContext.GetClientIPAddress();

            return new EventRecordDataDto
            {
                QueryString = param.QueryString,
                Data = param.Data,
                Header = header,
                RecordTime = DateTime.Now,
                ClientIPAddress = ipaddress.ToString(),
            };
        }
    }
}
