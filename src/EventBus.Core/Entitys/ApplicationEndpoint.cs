using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class ApplicationEndpoint : BaseEntity<ApplicationEndpoint>, IApplicationEndpoint
    {
        public string EndpointName { set; get; }

        public Uri EndpointUrl { set; get; }

        public Guid ApplicationId { set; get; }

        public ProtocolType NoticeProtocol { set; get; }

        public IApplication Application { set; get; }

        public IRetryPolicy[] FailedRetryPolicy { set; get; }

        public IRetryPolicy GetRetryPolicy(int retryCount = 1)
        {
            return new RetryPolicy();
        }
    }
}
