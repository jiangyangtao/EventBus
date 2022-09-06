using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class ApplicationEndpointController : BaseApiController
    {
        private readonly IApplicationProvider _applicationProvider;

        public ApplicationEndpointController(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        [HttpPost]
        public async Task<ApplicationEndpointDtoBase> Add([FromBody] ApplicationEndpointAddDto endpoint)
        {
            var applicationEndpoint = endpoint.GetApplicationEndpoint();
            var id = await _applicationProvider.AddOrUpdateApplicationEndpointAsync(applicationEndpoint);

            return new ApplicationEndpointDtoBase { applicationEndpointId = id };
        }

        [HttpDelete("{applicationEndpointId}")]
        public async Task<IActionResult> Remove(Guid applicationEndpointId)
        {
            if (applicationEndpointId == Guid.Empty) return NotFound();

            await _applicationProvider.RemoveApplicationEndpointAsync(applicationEndpointId);
            return NoContent();
        }
    }
}
