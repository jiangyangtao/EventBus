using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class EventController : BaseApiController
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEventRecordProvider _eventRecordProvider;

        public EventController(IEventProvider eventProvider, IEventRecordProvider eventRecordProvider)
        {
            _eventProvider = eventProvider;
            _eventRecordProvider = eventRecordProvider;
        }

        [HttpPut("{eventId}")]
        [HttpPost("{eventId}")]
        public async Task<IActionResult> Publish(Guid eventId)
        {
            if (eventId == Guid.Empty) return NotFound();

            var e = await _eventProvider.GetEventAsync(eventId, false);
            if (e == null) return NotFound();

            var ipAddress = HttpContext.GetClientIPAddress();
            var r = e.VerifyIPAddress(ipAddress);
            if (r == false) return NotFound();

            await _eventRecordProvider.PublishAsync(e.Id);
            return Ok();
        }

        [HttpPost]
        public async Task<EventDtoBase> Add([FromBody] EventAddDto e)
        {
            var id = await _eventProvider.AddOrUpdateAsync(e.GetEvent());
            return new EventDtoBase { EventId = id };
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            if (eventId == Guid.Empty) return NotFound();

            await _eventProvider.RemoveAsync(eventId);
            return NoContent();
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Modify([FromBody] EventAddDto e, Guid eventId)
        {
            if (eventId == Guid.Empty) return NotFound();

            await _eventProvider.AddOrUpdateAsync(e.GetEvent(eventId));
            return Ok();
        }

        [HttpGet("{eventId}")]
        public async Task<EventResult> Get(Guid eventId)
        {
            var e = await _eventProvider.GetEventAsync(eventId);
            if (e == null) return null;

            return new EventResult(e);
        }

        [HttpGet]
        public async Task<EventPaginationResult> List([FromQuery] EventQueryDto query)
        {
            var events = await _eventProvider.GetEventsAsync(query.startIndex, query.count, query.EventName);
            var count = await _eventProvider.GetEventCountAsync(query.EventName);

            return new EventPaginationResult(count, events);
        }
    }
}
