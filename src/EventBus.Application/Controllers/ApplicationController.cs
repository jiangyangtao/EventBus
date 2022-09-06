using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Application.Controllers
{
    public class ApplicationController : BaseApiController
    {
        private readonly IApplicationProvider _applicationProvider;

        public ApplicationController(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        [HttpPost]
        public async Task<ApplicationDtoBase> Add([FromBody] ApplicationDto application)
        {
            var id = await _applicationProvider.AddOrUpdateApplicationAsync(Guid.Empty, application.applicationName);
            return new ApplicationDtoBase() { applicationId = id };
        }

        [HttpDelete("{applicationId}")]
        public async Task<IActionResult> Remove(Guid applicationId)
        {
            if (applicationId == Guid.Empty) return NotFound();

            await _applicationProvider.RemoveApplicationAsync(applicationId);
            return NoContent();
        }

        [HttpPut("{applicationId}")]
        public async Task<IActionResult> Modify([FromBody] ApplicationDto application, Guid applicationId)
        {
            if (applicationId == Guid.Empty) return NotFound();

            var data = await _applicationProvider.GetApplicationAsync(applicationId);
            if (data == null) return NotFound();

            await _applicationProvider.AddOrUpdateApplicationAsync(applicationId, application.applicationName);
            return Ok();
        }

        [HttpGet]
        public async Task<ApplicationResult> Get([FromQuery] ApplicationDtoBase query)
        {
            var application = await _applicationProvider.GetApplicationAsync(query.applicationId);
            if (application == null) return null;

            return new ApplicationResult(application);
        }

        [HttpGet]
        public async Task<ApplicationPaginationResult> List([FromQuery] ApplicationQueryDto query)
        {
            var applications = await _applicationProvider.GetApplicationsAsync(query.startIndex, query.count, query.applicationName);
            var count = await _applicationProvider.GetApplicationCountAsync(query.applicationName);

            return new ApplicationPaginationResult(count, applications);
        }
    }
}
