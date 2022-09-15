using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EventBus.Core.Entitys
{
    public class SubscriptionRecord : BaseEntity<SubscriptionRecord>, ISubscriptionRecord
    {
        public SubscriptionRecord() { }

        public SubscriptionRecord(Guid eventId, IEventRecord eventRecord, ISubscription subscript)
        {
            EventId = eventId;
            EventRecordId = eventRecord.Id;
            EndpointName = subscript.EndpointName;
            EndpointUrl = subscript.EndpointUrl;
            SubscriptionProtocol = subscript.SubscriptionProtocol;
            FailedRetryPolicy = subscript.FailedRetryPolicy;
            RequestTimeout = subscript.RequestTimeout;

            SubscriptionHeader = eventRecord.Header;
            SubscriptionContent = eventRecord.BuilderHttpContent();
        }

        public Guid EventId { set; get; }

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

        public ProtocolType SubscriptionProtocol { set; get; }

        public int RequestTimeout { set; get; }

        [NotMapped]
        public IRetryPolicy[] FailedRetryPolicy
        {
            set
            {
                if (value.IsNullOrEmpty()) FailedRetryPolicyContent = string.Empty;
                else FailedRetryPolicyContent = JsonConvert.SerializeObject(value);
            }

            get
            {
                if (FailedRetryPolicyContent.IsNullOrEmpty()) return Array.Empty<RetryPolicy>();

                return JsonConvert.DeserializeObject<RetryPolicy[]>(FailedRetryPolicyContent);
            }
        }

        public string FailedRetryPolicyContent { set; get; }

        public bool SubscriptionResult { set; get; }

        [NotMapped]
        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { set; get; }

        private IDictionary<string, string> SubscriptionHeader { set; get; }

        public IDictionary<string, string> GetSubscriptionHeader() => SubscriptionHeader;

        private HttpContent SubscriptionContent { set; get; }

        public HttpContent GetSubscriptionContent() => SubscriptionContent;

        [NotMapped]
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Automatic;

        public IRetryPolicy GetRetryPolicy(int retryCount = 1)
        {
            if (FailedRetryPolicy.IsNullOrEmpty()) return new RetryPolicy(0, RetryBehavior.Discard);
            if (retryCount - 1 > FailedRetryPolicy.Length) return new RetryPolicy(0, RetryBehavior.Discard);

            return FailedRetryPolicy[retryCount - 1];
        }

        [NotMapped]
        public bool FailToRetry { set; get; } = true;

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <returns></returns>
        public async Task<EndpointSubscriptionRecord> Subscription(IHttpClientFactory httpClientFactory, HttpContent httpContent, IDictionary<string, string> header)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(RequestTimeout);
            if (header.NotNullAndEmpty())
            {
                foreach (var item in header)
                {
                    if (item.Key == "Content-Length") continue;

                    if (client.DefaultRequestHeaders.Contains(item.Key) == false)
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            var subscriptionTime = DateTime.Now;
            var watch = new Stopwatch();
            watch.Start();

            var response = await client.PostAsync(EndpointUrl, httpContent);
            watch.Stop();

            var content = await response.Content.ReadAsStringAsync();
            var responseStatus = (int)response.StatusCode;

            return new EndpointSubscriptionRecord
            {
                SubscriptionRecordId = Id,
                SubscriptionType = SubscriptionType,
                SubscriptionTime = subscriptionTime,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ResponseStatus = responseStatus.ToString(),
                ResponseStatusCode = response.StatusCode.ToString(),
                ResponseHeaders = response.Headers.ToDictionary(a => a.Key, a => a.Value.ToString()),
                ResponseContent = content,
                ResponseTime = DateTime.Now,
                UsageTime = watch.ElapsedMilliseconds,
            };
        }

        public RetryData GetRetryData(IRetryPolicy policy)
        {
            return new RetryData(EventId, EventRecordId, Id, policy.RetryDelayUnit, policy.RetryDelayCount);
        }
    }
}
