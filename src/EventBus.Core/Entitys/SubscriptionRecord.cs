﻿using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EventBus.Core.Entitys
{
    internal class SubscriptionRecord : BaseEntity<SubscriptionRecord>, Abstractions.IModels.SubscriptionRecord
    {
        public SubscriptionRecord() { }

        public SubscriptionRecord(Guid eventId, Abstractions.IModels.EventRecord eventRecord, Abstractions.IModels.ApplicationEndpoint endpoint)
        {
            EventId = eventId;
            EventRecordId = eventRecord.Id;
            EndpointName = endpoint.EndpointName;
            EndpointUrl = endpoint.EndpointUrl;
            SubscriptionProtocol = endpoint.SubscriptionProtocol;
            FailedRetryPolicy = endpoint.FailedRetryPolicy;

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
        public Abstractions.IModels.RetryPolicy[] FailedRetryPolicy
        {
            set
            {
                if (value.IsNullOrEmpty()) FailedRetryPolicyString = string.Empty;
                else FailedRetryPolicyString = JsonConvert.SerializeObject(value);
            }

            get
            {
                if (FailedRetryPolicyString.IsNullOrEmpty()) return Array.Empty<RetryPolicy>();

                return JsonConvert.DeserializeObject<RetryPolicy[]>(FailedRetryPolicyString);
            }
        }

        public string FailedRetryPolicyString { set; get; }

        public bool SubscriptionResult { set; get; }

        [NotMapped]
        public Abstractions.IModels.EndpointSubscriptionRecord[] EndpointSubscriptionRecords { set; get; }

        private IDictionary<string, string> SubscriptionHeader { set; get; }

        public IDictionary<string, string> GetSubscriptionHeader() => SubscriptionHeader;

        private HttpContent SubscriptionContent { set; get; }

        public HttpContent GetSubscriptionContent() => SubscriptionContent;

        [NotMapped]
        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Automatic;

        public Abstractions.IModels.RetryPolicy GetRetryPolicy(int retryCount = 1)
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
        public async Task<EndpointSubscriptionRecord> Subscription(IHttpClientFactory httpClientFactory, HttpContent httpContent, IDictionary<string, string> header)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(RequestTimeout);
            if (header.NotNullAndEmpty())
            {
                foreach (var item in header)
                {
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

            return new EndpointSubscriptionRecord
            {
                SubscriptionType = SubscriptionType,
                SubscriptionTime = subscriptionTime,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ResponseStatucCode = response.StatusCode.ToString(),
                ResponseContent = content,
                ResponseTime = DateTime.Now,
                UsageTime = watch.ElapsedMilliseconds,
            };
        }

        public RetryData GetRetryData(Abstractions.IModels.RetryPolicy policy)
        {
            return new RetryData(EventId, EventRecordId, Id, policy.RetryDelayUnit, policy.RetryDelayCount);
        }
    }
}
