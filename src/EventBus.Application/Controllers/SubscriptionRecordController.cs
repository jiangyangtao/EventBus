using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class SubscriptionRecordController : BaseApiController
    {
        private readonly ISubscriptionProvider _subscriptionProvider;

        public SubscriptionRecordController(ISubscriptionProvider subscriptionProvider)
        {
            _subscriptionProvider = subscriptionProvider;
        }

        [HttpGet("{eventRecordId}")]
        public async Task<SubscriptionRecordResult[]> List(Guid eventRecordId)
        {
            var records = await _subscriptionProvider.GetSubscriptionRecordsAsync(eventRecordId);
            if (records.IsNullOrEmpty()) return Array.Empty<SubscriptionRecordResult>();

            return records.Select(a => new SubscriptionRecordResult(a)).ToArray();
        }
    }
}
