using EventBus.Abstractions.IProviders;
using EventBus.Application.Controllers.Base;
using EventBus.Application.Dto;
using EventBus.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace EventBus.Application.Controllers
{
    public class ApplicationController : BaseApiController
    {
        private readonly IApplicationProvider _applicationProvider;

        public ApplicationController(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        [HttpGet("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ApplicationDtoBase> Add([FromBody] ApplicationDto application)
        {
            var id = await _applicationProvider.AddOrUpdateApplicationAsync(Guid.Empty, application.ApplicationName);
            return new ApplicationDtoBase() { ApplicationId = id };
        }

        [HttpDelete("{applicationId}")]
        public async Task<IActionResult> Delete(Guid applicationId)
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

            await _applicationProvider.AddOrUpdateApplicationAsync(applicationId, application.ApplicationName);
            return Ok();
        }

        [HttpGet("{applicationId}")]
        public async Task<ApplicationResult> Get(Guid applicationId)
        {
            var application = await _applicationProvider.GetApplicationAsync(applicationId);
            if (application == null) ResponseNotFound();

            return new ApplicationResult(application);
        }

        [HttpGet]
        public async Task<ApplicationPaginationResult> List([FromQuery] ApplicationQueryDto query)
        {
            var applications = await _applicationProvider.GetApplicationsAsync(query.startIndex, query.count, query.ApplicationName);
            var count = await _applicationProvider.GetApplicationCountAsync(query.ApplicationName);

            return new ApplicationPaginationResult(count, applications);
        }

        [HttpGet]
        public async Task<SuggestionResult[]> Suggestion([FromQuery] ApplicationSuggestionDto query)
        {
            var applications = await _applicationProvider.GetApplicationsAsync(query.ApplicationName);
            if (applications.IsNullOrEmpty()) return Array.Empty<SuggestionResult>();

            return applications.Select(a => new SuggestionResult(a.ApplicationName, a.Id.ToString())).ToArray();
        }
    }
}
