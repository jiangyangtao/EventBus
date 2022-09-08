using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class SubscriptionController : BaseApiController
    {
        private readonly IEventProvider _eventProvider;
        private readonly ISubscriptionProvider _subscriptionProvider;
        private readonly IApplicationProvider _applicationProvider;

        public SubscriptionController(IEventProvider eventProvider, IApplicationProvider applicationProvider, ISubscriptionProvider subscriptionProvider)
        {
            _eventProvider = eventProvider;
            _applicationProvider = applicationProvider;
            _subscriptionProvider = subscriptionProvider;
        }

        [HttpPost]
        public async Task<SubscriptionDtoBase> Add([FromBody] SubscriptionAddDto subscription)
        {
            var e = await _eventProvider.GetEventAsync(subscription.EventId, false);
            if (e == null) ResponseNotFound("未找到事件");

            var endpoint = await _applicationProvider.GetApplicationEndpointAsync(subscription.ApplicationEndpointId);
            if (endpoint == null) ResponseNotFound("未找到接入点");

            var id = await _subscriptionProvider.AddAsync(subscription.EventId, subscription.ApplicationEndpointId);
            return new SubscriptionDtoBase { SubscriptionId = id };
        }

        [HttpDelete("{subscriptionId}")]
        public async Task<IActionResult> Delete(Guid subscriptionId)
        {
            if (subscriptionId == Guid.Empty) return NotFound();

            await _subscriptionProvider.RemoveAsync(subscriptionId);
            return Ok();
        }

        [HttpGet("{subscriptionId}")]
        public async Task<SubscriptionResult> Get(Guid subscriptionId)
        {
            var subscription = await _subscriptionProvider.GetSubscriptionAsync(subscriptionId);
            if (subscription == null) ResponseNotFound();

            var e = await _eventProvider.GetEventAsync(subscription.EventId);
            return new SubscriptionResult(subscription, e);
        }

        [HttpGet]
        public async Task<SubscriptionPaginationResult> List([FromQuery] SubscriptionQueryDto query)
        {
            var subscriptions = await _subscriptionProvider.GetSubscriptionsAsync(query.EventId, query.EndpointName, query.startIndex, query.count);
            var count = await _subscriptionProvider.GetSubscriptionCountAsync(query.EventId, query.EndpointName);

            return new SubscriptionPaginationResult(count, subscriptions);
        }
    }
}
