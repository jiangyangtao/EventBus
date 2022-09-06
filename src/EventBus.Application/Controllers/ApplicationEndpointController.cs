﻿using EventBus.Abstractions.IProviders;
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

            return new ApplicationEndpointDtoBase { ApplicationEndpointId = id };
        }

        [HttpDelete("{applicationEndpointId}")]
        public async Task<IActionResult> Delete(Guid applicationEndpointId)
        {
            if (applicationEndpointId == Guid.Empty) return NotFound();

            await _applicationProvider.RemoveApplicationEndpointAsync(applicationEndpointId);
            return NoContent();
        }

        [HttpPut("{applicationEndpointId}")]
        public async Task<IActionResult> Modify([FromBody] ApplicationEndpointAddDto endpoint, Guid applicationEndpointId)
        {
            if (applicationEndpointId == Guid.Empty) return NotFound();

            var applicationEndpoint = await _applicationProvider.GetApplicationEndpointAsync(applicationEndpointId);
            if (applicationEndpoint == null) return NotFound();

            await _applicationProvider.AddOrUpdateApplicationEndpointAsync(endpoint.GetApplicationEndpoint(applicationEndpointId));
            return Ok();
        }

        [HttpGet]
        public async Task<ApplicationEndpointResult> Get([FromQuery] ApplicationEndpointDtoBase query)
        {
            var applicationEndpoint = await _applicationProvider.GetApplicationEndpointAsync(query.ApplicationEndpointId);
            if (applicationEndpoint == null) return null;

            return new ApplicationEndpointResult(applicationEndpoint);
        }

        [HttpGet]
        public async Task<ApplicationEndpointPaginationResult> List([FromQuery] ApplicationEndpointQueryDto query)
        {
            var applicationEndpoints = await _applicationProvider.GetApplicationEndpointsAsync(query.startIndex, query.count, query.EndpointName);
            var count = await _applicationProvider.GetApplicationCountAsync(query.EndpointName);

            return new ApplicationEndpointPaginationResult(count, applicationEndpoints);
        }
    }
}
