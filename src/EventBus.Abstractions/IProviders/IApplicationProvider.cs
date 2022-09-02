using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 应用提供者
    /// </summary>
    public interface IApplicationProvider
    {
        Task AddOrUpdateApplicationAsync(IApplication application);

        Task AddOrUpdateApplicationEndpointAsync(IApplicationEndpoint applicationEndpoint);

        Task RemoveApplicationAsync(IApplication application);

        Task RemoveApplicationEndpointAsync(IApplicationEndpoint endpoint);

        Task<IApplication> GetApplicationAsync(Guid applicationId);

        Task<IApplication> GetApplicationAsync(string applicationName);

        Task<IApplication[]> GetApplicationsAsync();

        Task<IApplication[]> GetApplicationsAsync(int start, int count, string applicationName);

        Task<IApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName);
    }
}
