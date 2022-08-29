using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class ApplicationEndpoint : BaseEntity<ApplicationEndpoint>, IApplicationEndpoint
    {
        public ApplicationEndpoint() { }

        public ApplicationEndpoint(IApplicationEndpoint endpoint)
        {
            EndpointName = endpoint.EndpointName;
            EndpointUrl = endpoint.EndpointUrl;
            ApplicationId = endpoint.Application.Id;
            SubscriptionProtocol = endpoint.SubscriptionProtocol;
            Application = endpoint.Application;
            FailedRetryPolicy = endpoint.FailedRetryPolicy;
        }

        public string EndpointName { set; get; }

        [NotMapped]
        public Uri EndpointUrl
        {
            set
            {
                EndpointUrlString = value.ToString();
            }
            get
            {
                if (EndpointUrlString == null) return null;

                return new Uri(EndpointUrlString);
            }
        }

        public string EndpointUrlString { set; get; }

        public int RequestTimeout { set; get; }

        public Guid ApplicationId { set; get; }

        public ProtocolType SubscriptionProtocol { set; get; }

        [NotMapped]
        public IApplication Application { set; get; }

        [NotMapped]
        public IRetryPolicy[] FailedRetryPolicy
        {
            set
            {
                FailedRetryPolicyString = JsonConvert.SerializeObject(value);
            }

            get
            {
                if (FailedRetryPolicyString.IsNullOrEmpty()) return Array.Empty<RetryPolicy>();

                return JsonConvert.DeserializeObject<IRetryPolicy[]>(FailedRetryPolicyString);
            }
        }

        public string FailedRetryPolicyString { set; get; }


    }
}
