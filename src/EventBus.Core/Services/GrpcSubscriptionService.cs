using EventBus.Client.Protos;
using EventBus.Core.Entitys;
using EventBus.Core.Interceptors;
using EventBus.Extensions;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using System.Diagnostics;

namespace EventBus.Core.Services
{
    internal class GrpcSubscriptionService
    {
        private readonly EventRecord _record;
        private readonly EventRecordSubscription _subscription;
        private readonly GrpcInterceptor _grpcInterceptor;

        public GrpcSubscriptionService(IServiceProvider serviceProvider, EventRecord record, EventRecordSubscription subscription)
        {
            _record = record;
            _subscription = subscription;
            _grpcInterceptor = new GrpcInterceptor(serviceProvider, subscription);
        }

        private GrpcChannel _GrpcChannel { set; get; }

        public GrpcChannel GrpcChannel
        {
            get
            {
                if (_GrpcChannel == null || _GrpcChannel.State > Grpc.Core.ConnectivityState.Ready)
                {
                    using var channel = GrpcChannel.ForAddress(_subscription.EndpointUrl.GetSchemeHost());

                    channel.Intercept(_grpcInterceptor);
                    _GrpcChannel = channel;
                }

                return GrpcChannel;
            }
        }

        public async Task<EndpointSubscriptionRecord> SubscriptionAsync()
        {
            var subscriptionTime = DateTime.Now;
            var watch = new Stopwatch();
            watch.Start();

            var subscriptionClient = new SubscriptionProto.SubscriptionProtoClient(_GrpcChannel);
            var result = await subscriptionClient.PushAsync(new PushModel
            {
                Data = _record.Data,
                QueryString = _record.QueryString,
            });
            watch.Stop();

            return new EndpointSubscriptionRecord
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
    }
}
