using EventBus.Abstractions.IProviders;
using EventBus.API.Dto;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.API.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventProvider _eventProvider;
        private readonly IEventRecordProvider _eventRecordProvider;

        public EventController(IEventProvider eventProvider, IEventRecordProvider eventRecordProvider)
        {
            _eventProvider = eventProvider;
            _eventRecordProvider = eventRecordProvider;
        }

        [HttpPut("/{eventId}")]
        [HttpPost("/Put/{eventId}")]
        public async Task<IActionResult> Put([FromBody] object data, [FromQuery] Guid eventId)
        {
            var e = await _eventProvider.GetEventAsync(eventId, false);
            if (e == null) return NotFound();

            var ipAddress = HttpContext.GetClientIPAddress();
            var r = e.VerifyIPAddress(ipAddress);
            if (r == false) return NotFound();

            var record = new EventRecordDto(e.Id, data, Request);
            await _eventRecordProvider.PublishAsync(record);
            return Ok();
        }
    }
}
