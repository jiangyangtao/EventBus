using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class ApplicationEndpointDtoBase
    {
        public Guid applicationEndpointId { set; get; }
    }

    public class ApplicationEndpointDto : IApplicationEndpoint
    {
        public string EndpointName { set; get; }

        public Uri EndpointUrl { set; get; }

        public ProtocolType SubscriptionProtocol { set; get; }

        public int RequestTimeout { set; get; }

        public Guid ApplicationId { set; get; }

        public IApplication Application { set; get; }

        public IRetryPolicy[] FailedRetryPolicy { set; get; }

        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }
    }

    public class ApplicationEndpointAddDto
    {
        [Required, MaxLength(100)]
        public string endpointName { set; get; }

        public Uri endpointUrl { set; get; }

        public ProtocolType? subscriptionProtocol { set; get; }

        public int requestTimeout { set; get; }

        public Guid applicationId { set; get; }

        public RetryPolicyDto[] failedRetryPolicy { set; get; }

        private ApplicationEndpointDto BuildApplicationEndpoint()
        {
            return new ApplicationEndpointDto
            {
                EndpointName = endpointName,
                EndpointUrl = endpointUrl,
                SubscriptionProtocol = subscriptionProtocol.Value,
                RequestTimeout = requestTimeout,
                ApplicationId = applicationId,
                FailedRetryPolicy = failedRetryPolicy
            };
        }

        virtual internal IApplicationEndpoint GetApplicationEndpoint() => BuildApplicationEndpoint();

        virtual internal IApplicationEndpoint GetApplicationEndpoint(Guid id)
        {
            var applicationEndpoint = BuildApplicationEndpoint();
            applicationEndpoint.Id = id;

            return applicationEndpoint;
        }
    }

    public class ApplicationEndpointModifyDto : ApplicationEndpointAddDto
    {
        public Guid applicationEndpointId { set; get; }

        internal override IApplicationEndpoint GetApplicationEndpoint() => GetApplicationEndpoint(applicationEndpointId);
    }

    public class ApplicationEndpointResult : ApplicationEndpointModifyDto
    {
        public ApplicationEndpointResult(IApplicationEndpoint applicationEndpoint)
        {
            applicationEndpointId = applicationEndpoint.Id;
            applicationId = applicationEndpoint.ApplicationId;
            endpointName = applicationEndpoint.EndpointName;
            endpointUrl = applicationEndpoint.EndpointUrl;
            subscriptionProtocol = applicationEndpoint.SubscriptionProtocol;
            requestTimeout = applicationEndpoint.RequestTimeout;
            failedRetryPolicy = applicationEndpoint.FailedRetryPolicy.Select(a => new RetryPolicyDto(a)).ToArray();
        }
    }

    public class ApplicationEndpointPaginationResult : PaginationResult<ApplicationEndpointResult>
    {
        public ApplicationEndpointPaginationResult(long count, IApplicationEndpoint[] applicationEndpoints) : base(count)
        {
            List = applicationEndpoints.Select(a => new ApplicationEndpointResult(a)).ToArray();
        }
    }
}
