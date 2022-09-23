using EventBus.Abstractions.IModels;
using EventBus.Extensions;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus.Subscription.Interceptors
{
    internal class GrpcInterceptor : Interceptor
    {
        private readonly ILogger _logger;
        private readonly IEventRecordSubscription _subscription;

        public IDictionary<string, string> ResponseHeaders { get; private set; }

        public string ResponseContent { get; private set; }

        public TaskStatus TaskStatus { get; private set; }

        public int TaskStatusNumber { get; private set; }

        public GrpcInterceptor(IServiceProvider serviceProvider, IEventRecordSubscription subscription)
        {
            _logger = serviceProvider.GetRequiredService<ILogger>();
            _subscription = subscription;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            var pathParams = _subscription.EndpointUrl.PathAndQuery.Split("/").Where(a => a.NotNullAndEmpty()).ToArray();
            var newContext = CreateContext(context, pathParams[0], pathParams[1]);
            var call = continuation(request, newContext);

            var result = new AsyncUnaryCall<TResponse>(
                        HandleResponse(call.ResponseAsync),
                        HandleHeaders(call.ResponseHeadersAsync),
                        call.GetStatus,
                        call.GetTrailers,
                        call.Dispose);
            return result;
        }

        private static ClientInterceptorContext<TRequest, TResponse> CreateContext<TRequest, TResponse>(ClientInterceptorContext<TRequest, TResponse> context, string serviceName, string methodName)
         where TRequest : class
         where TResponse : class
        {
            var method = CreateMethod(context.Method, serviceName, methodName);
            return new ClientInterceptorContext<TRequest, TResponse>(method, context.Host, context.Options);
        }

        private static Method<TRequest, TResponse> CreateMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, string serviceName, string methodName)
        {
            return new Method<TRequest, TResponse>(method.Type, serviceName, methodName, method.RequestMarshaller, method.ResponseMarshaller);
        }

        private async Task<Metadata> HandleHeaders(Task<Metadata> responseHeadersAsync)
        {
            var headers = await responseHeadersAsync;
            ResponseHeaders = headers.ToDictionary(a => a.Key, a => a.Value);
            return headers;
        }

        private async Task<TResponse> HandleResponse<TResponse>(Task<TResponse> response)
        {
            try
            {
                var r = await response;

                TaskStatus = response.Status;
                TaskStatusNumber = (int)response.Status;
                ResponseContent = r.ToString();

                return r;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("response error", ex);
            }
        }
    }
}
