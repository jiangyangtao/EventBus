using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class SubscriptionRecord : BaseEntity<SubscriptionRecord>, ISubscriptionRecord
    {
        public SubscriptionRecord() { }

        public SubscriptionRecord(IEventRecord eventRecord, IApplicationEndpoint endpoint)
        {
            EventRecordId = eventRecord.Id;
            EndpointName = endpoint.EndpointName;
            EndpointUrl = endpoint.EndpointUrl;
            NoticeProtocol = endpoint.NoticeProtocol;
            FailedRetryPolicy = endpoint.FailedRetryPolicy;
        }

        public Guid EventRecordId { get; set; }

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
                if (EndpointUrlString.IsNullOrEmpty()) return null;

                return new Uri(EndpointUrlString);
            }
        }

        public string EndpointUrlString { set; get; }

        public ProtocolType NoticeProtocol { set; get; }

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

        public bool SubscriptionResult { set; get; }

        [NotMapped]
        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { set; get; }

        public IRetryPolicy GetRetryPolicy(int retryCount = 1)
        {
            if (FailedRetryPolicy.IsNullOrEmpty()) return new RetryPolicy(0, RetryBehavior.Discard);
            if (retryCount - 1 > FailedRetryPolicy.Length) return new RetryPolicy(0, RetryBehavior.Discard);

            return FailedRetryPolicy[retryCount - 1];
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <returns></returns>
        public async Task<EndpointSubscriptionRecord> Subscription(IHttpClientFactory httpClientFactory, EventRecord record)
        {
            var client = httpClientFactory.CreateClient();
            var response = await client.PostAsync(EndpointUrl, record.BuilderHttpContent());

        }
    }
}
