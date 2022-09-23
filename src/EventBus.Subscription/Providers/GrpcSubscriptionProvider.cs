using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IProviders;
using EventBus.Client.Protos;
using EventBus.Extensions;
using EventBus.Subscription.Interceptors;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using System.Diagnostics;

namespace EventBus.Subscription.Providers
{
    internal class GrpcSubscriptionProvider : IAsyncSubscriptionProvider
    {
        private readonly IEventRecord _record;
        private readonly IEventRecordSubscription _subscription;
        private readonly GrpcInterceptor _grpcInterceptor;

        public GrpcSubscriptionProvider(IServiceProvider serviceProvider, IEventRecordSubscription subscription)
        {
            _record = subscription.EventRecord;
            _subscription = subscription;
            _grpcInterceptor = new GrpcInterceptor(serviceProvider, subscription);
        }

        public async Task<IEndpointSubscriptionRecord> SubscriptionAsync()
        {
            var subscriptionTime = DateTime.Now;
            var watch = new Stopwatch();
            watch.Start();

            var subscriptionClient = new SubscriptionProto.SubscriptionProtoClient(GrpcChannel);
            var result = await subscriptionClient.PushAsync(new PushModel
            {
                Data = _record.Data,
                QueryString = _record.QueryString,
            });
            watch.Stop();

            return new EndpointSubscriptionRecordData
            {
                EventRecordSubscriptionId = _subscription.Id,
                SubscriptionType = _subscription.SubscriptionType,
                SubscriptionTime = subscriptionTime,
                IsSuccessStatusCode = result.Result,
                ResponseStatus = _grpcInterceptor.TaskStatusNumber.ToString(),
                ResponseStatusCode = _grpcInterceptor.TaskStatus.ToString(),
                ResponseHeaders = _grpcInterceptor.ResponseHeaders,
                ResponseContent = _grpcInterceptor.ResponseContent,
                ResponseTime = DateTime.Now,
                UsageTime = watch.ElapsedMilliseconds,
            };
        }


        public GrpcChannel GrpcChannel
        {
            get
            {
                using var channel = GrpcChannel.ForAddress(_subscription.EndpointUrl.GetSchemeHost());
                channel.Intercept(_grpcInterceptor);
                return channel;
            }
        }
    }
}
