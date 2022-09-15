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

        [HttpPost("{subscriptionRecordId}")]
        public async Task<IActionResult> Subscription(Guid subscriptionRecordId)
        {
            var record = await _eventRecordProvider.GetSubscriptionRecordAsync(subscriptionRecordId);
            if (record == null) return NotFound();

            await _eventRecordProvider.SubscriptionAsync(subscriptionRecordId);
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
        public async Task<SubscriptionRecordResult[]> GetSubscriptionRecord(Guid eventRecordId)
        {
            var records = await _eventRecordProvider.GetSubscriptionRecordsAsync(eventRecordId);
            if (records.IsNullOrEmpty()) return Array.Empty<SubscriptionRecordResult>();

            return records.Select(a => new SubscriptionRecordResult(a)).ToArray();
        }

        [HttpGet("{subscriptionRecordId}")]
        public async Task<EndpointSubscriptionRecordResult[]> GetEndpointSubscriptionRecord(Guid subscriptionRecordId)
        {
            var records = await _eventRecordProvider.GetEndpointSubscriptionRecordsAsync(subscriptionRecordId);
            if (records.IsNullOrEmpty()) return Array.Empty<EndpointSubscriptionRecordResult>();

            return records.Select(a => new EndpointSubscriptionRecordResult(a)).ToArray();
        }
    }
}
