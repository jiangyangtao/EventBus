﻿using EventBus.Abstractions.IProviders;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
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
        [HttpPost("/{eventId}")]
        public async Task<IActionResult> Put(Guid eventId)
        {
            var e = await _eventProvider.GetEventAsync(eventId, false);
            if (e == null) return NotFound();

            var ipAddress = HttpContext.GetClientIPAddress();
            var r = e.VerifyIPAddress(ipAddress);
            if (r == false) return NotFound();

            await _eventRecordProvider.PublishAsync(e.Id);
            return Ok();
        }
    }
}