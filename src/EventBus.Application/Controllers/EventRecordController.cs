using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class EventRecordController : BaseApiController
    {
        private readonly IEventRecordProvider _eventRecordProvider;

        public EventRecordController(IEventRecordProvider eventRecordProvider)
        {
            _eventRecordProvider = eventRecordProvider;
        }

        [HttpGet]
        public async Task<EventRecordPaginationResult> List([FromQuery] EventRecordQueryDto query)
        {
            var records = await _eventRecordProvider.GetEventRecordsAsync(query.startIndex, query.count, query.BeginTime, query.EndTime);
            var count = await _eventRecordProvider.GetEventRecordCountAsync(query.BeginTime, query.EndTime);

            return new EventRecordPaginationResult(count, records);
        }
    }
}
