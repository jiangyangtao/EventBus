using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 异步订阅提供者
    /// </summary>
    public interface IAsyncSubscriptionProvider
    {
        Task<IEndpointSubscriptionRecord> SubscriptionAsync();
    }
}
