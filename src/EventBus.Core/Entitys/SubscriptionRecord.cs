﻿using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EventBus.Core.Entitys
{
    internal class SubscriptionRecord : BaseEntity<SubscriptionRecord>, ISubscriptionRecord
    {
        public SubscriptionRecord() { }

        public SubscriptionRecord(Guid eventId, IEventRecord eventRecord, IApplicationEndpoint endpoint)
        {
            EventId = eventId;
            EventRecordId = eventRecord.Id;
            EndpointName = endpoint.EndpointName;
            EndpointUrl = endpoint.EndpointUrl;
            SubscriptionProtocol = endpoint.SubscriptionProtocol;
            FailedRetryPolicy = endpoint.FailedRetryPolicy;

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
                if (value.IsNullOrEmpty()) FailedRetryPolicyString = string.Empty;
                else FailedRetryPolicyString = JsonConvert.SerializeObject(value);
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

        private HttpContent SubscriptionContent { set; get; }

        public HttpContent GetSubscriptionContent()
        {
            return SubscriptionContent;
        }

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <returns></returns>
        public async Task<EndpointSubscriptionRecord> Subscription(IHttpClientFactory httpClientFactory, HttpContent httpContent)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(RequestTimeout);

            var subscriptionTime = DateTime.Now;
            var watch = new Stopwatch();
            watch.Start();

            var response = await client.PostAsync(EndpointUrl, httpContent);
            watch.Stop();

            var content = await response.Content.ReadAsStringAsync();

            return new EndpointSubscriptionRecord
            {
                SubscriptionType = SubscriptionType.Automatic,
                SubscriptionTime = subscriptionTime,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ResponseStatucCode = response.StatusCode.ToString(),
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
