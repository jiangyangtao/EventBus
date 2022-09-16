using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class RetryController : BaseApiController
    {
        private readonly IRetryProvider _retryProvider;

        public RetryController(IRetryProvider retryProvider)
        {
            _retryProvider = retryProvider;
        }


        [HttpGet("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPut("{retryDataId}")]
        public async Task<IActionResult> Retry(Guid retryDataId)
        {
            var data = await _retryProvider.GetRetryAsync(retryDataId);
            if (data == null) return NotFound();

            await _retryProvider.RetryAsync(retryDataId);
            return Ok();
        }


        [HttpDelete("{retryDataId}")]
        public async Task<IActionResult> Delete(Guid retryDataId)
        {
            if (retryDataId == Guid.Empty) return NotFound();

            await _retryProvider.RemoveAsync(retryDataId);
            return NoContent();
        }


        [HttpGet]
        public async Task<RetryDataPaginationResult> List([FromQuery] RetryDataQueryDto query)
        {
            var retrys = await _retryProvider.GetRetryDatasAsync(query.EventName, query.EndpointName, query.startIndex, query.count);
            var count = await _retryProvider.GetRetryDataCountAsync(query.EventName, query.EndpointName);

            return new RetryDataPaginationResult(count, retrys);
        }
    }
}
