using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class ApplicationEndpointDtoBase
    {
        public Guid ApplicationEndpointId { set; get; }
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
        public string EndpointName { set; get; }

        public Uri EndpointUrl { set; get; }

        public ProtocolType? SubscriptionProtocol { set; get; }

        public int RequestTimeout { set; get; }

        public Guid ApplicationId { set; get; }

        public RetryPolicyDto[] FailedRetryPolicy { set; get; }

        private ApplicationEndpointDto BuildApplicationEndpoint()
        {
            return new ApplicationEndpointDto
            {
                EndpointName = EndpointName,
                EndpointUrl = EndpointUrl,
                SubscriptionProtocol = SubscriptionProtocol.Value,
                RequestTimeout = RequestTimeout,
                ApplicationId = ApplicationId,
                FailedRetryPolicy = FailedRetryPolicy
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

    public class ApplicationEndpointQueryDto : PagingParameter
    {
        [Required, MaxLength(100)]
        public string EndpointName { set; get; }
    }

    public class ApplicationEndpointResult : ApplicationEndpointAddDto
    {
        public ApplicationEndpointResult(IApplicationEndpoint applicationEndpoint)
        {
            ApplicationEndpointId = applicationEndpoint.Id;
            ApplicationId = applicationEndpoint.ApplicationId;
            EndpointName = applicationEndpoint.EndpointName;
            EndpointUrl = applicationEndpoint.EndpointUrl;
            SubscriptionProtocol = applicationEndpoint.SubscriptionProtocol;
            RequestTimeout = applicationEndpoint.RequestTimeout;
            FailedRetryPolicy = applicationEndpoint.FailedRetryPolicy.Select(a => new RetryPolicyDto(a)).ToArray();
        }

        public Guid ApplicationEndpointId { set; get; }
    }

    public class ApplicationEndpointPaginationResult : PaginationResult<ApplicationEndpointResult>
    {
        public ApplicationEndpointPaginationResult(long count, IApplicationEndpoint[] applicationEndpoints) : base(count)
        {
            List = applicationEndpoints.Select(a => new ApplicationEndpointResult(a)).ToArray();
        }
    }
}
