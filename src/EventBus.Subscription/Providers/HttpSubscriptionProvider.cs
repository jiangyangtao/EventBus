using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace EventBus.Subscription.Providers
{
    internal class HttpSubscriptionProvider : IAsyncSubscriptionProvider
    {
        private readonly IEventRecordSubscription _subscription;
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpSubscriptionProvider(IServiceProvider serviceProvider, IEventRecordSubscription subscription)
        {
            _subscription = subscription;
            _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        }

        public async Task<IEndpointSubscriptionRecord> SubscriptionAsync()
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(_subscription.RequestTimeout);

            var header = _subscription.EventRecord.Header;
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

            var response = await client.PostAsync(_subscription.EndpointUrl, _subscription.EventRecord.BuilderHttpContent());
            watch.Stop();

            var content = await response.Content.ReadAsStringAsync();
            var responseStatus = (int)response.StatusCode;

            return new EndpointSubscriptionRecordData
            {
                EventRecordSubscriptionId = _subscription.Id,
                SubscriptionType = _subscription.SubscriptionType,
                SubscriptionTime = subscriptionTime,
                IsSuccessStatusCode = response.IsSuccessStatusCode,
                ResponseStatus = responseStatus.ToString(),
                ResponseStatusCode = response.StatusCode.ToString(),
                ResponseHeaders = response.Headers.ToDictionary(a => a.Key, a => string.Join(",", a.Value)),
                ResponseContent = content,
                ResponseTime = DateTime.Now,
                UsageTime = watch.ElapsedMilliseconds,
            };
        }
    }
}
