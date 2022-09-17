using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using EventBus.Extensions;
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

        [HttpGet("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("{eventRecordSubscriptionId}")]
        public async Task<IActionResult> Subscription(Guid eventRecordSubscriptionId)
        {
            var record = await _eventRecordProvider.GetEventRecordSubscriptionAsync(eventRecordSubscriptionId);
            if (record == null) return NotFound();

            await _eventRecordProvider.SubscriptionAsync(eventRecordSubscriptionId);
            return Ok();
        }

        [HttpGet]
        public async Task<EventRecordPaginationResult> List([FromQuery] EventRecordQueryDto query)
        {
            var records = await _eventRecordProvider.GetEventRecordsAsync(query.startIndex, query.count, query.BeginTime, query.EndTime);
            var count = await _eventRecordProvider.GetEventRecordCountAsync(query.BeginTime, query.EndTime);

            return new EventRecordPaginationResult(count, records);
        }

        [HttpGet("{eventRecordId}")]
        public async Task<EventRecordSubscriptionResult[]> GetEventRecordSubscription(Guid eventRecordId)
        {
            var records = await _eventRecordProvider.GetEventRecordSubscriptionsAsync(eventRecordId);
            if (records.IsNullOrEmpty()) return Array.Empty<EventRecordSubscriptionResult>();

            return records.Select(a => new EventRecordSubscriptionResult(a)).ToArray();
        }

        [HttpGet("{eventRecordSubscriptionId}")]
        public async Task<EndpointSubscriptionRecordResult[]> GetEndpointSubscriptionRecord(Guid eventRecordSubscriptionId)
        {
            var records = await _eventRecordProvider.GetEndpointSubscriptionRecordsAsync(eventRecordSubscriptionId);
            if (records.IsNullOrEmpty()) return Array.Empty<EndpointSubscriptionRecordResult>();

            return records.Select(a => new EndpointSubscriptionRecordResult(a)).ToArray();
        }
    }
}
