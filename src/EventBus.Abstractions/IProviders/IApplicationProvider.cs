using EventBus.Abstractions.IModels;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 应用提供者
    /// </summary>
    public interface IApplicationProvider
    {
        Task<Guid> AddOrUpdateApplicationAsync(Guid applicationId, string applicationName);

        Task<Guid> AddOrUpdateApplicationEndpointAsync(IApplicationEndpoint applicationEndpoint);

        Task RemoveApplicationAsync(Guid applicationId);

        Task RemoveApplicationEndpointAsync(Guid applicationEndpointId);

        Task<IApplication> GetApplicationAsync(Guid applicationId);

        Task<IApplication> GetApplicationAsync(string applicationName);

        Task<IApplication[]> GetApplicationsAsync();

        Task<IApplication[]> GetApplicationsAsync(int start, int count, string applicationName);

        Task<long> GetApplicationCountAsync(string applicationName);

        Task<IApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId);

        Task<IApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName);
    }
}
