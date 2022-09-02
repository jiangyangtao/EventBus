using EventBus.Abstractions.Models;

namespace EventBus.Abstractions.IProviders
{
    /// <summary>
    /// 应用提供者
    /// </summary>
    public interface IApplicationProvider
    {
        Task AddOrUpdateApplicationAsync(Application application);

        Task AddOrUpdateApplicationEndpointAsync(ApplicationEndpoint applicationEndpoint);

        Task RemoveApplicationAsync(Application application);

        Task RemoveApplicationEndpointAsync(ApplicationEndpoint endpoint);

        Task<Application> GetApplicationAsync(Guid applicationId);

        Task<Application> GetApplicationAsync(string applicationName);

        Task<Application[]> GetApplicationsAsync();

        Task<Application[]> GetApplicationsAsync(int start, int count, string applicationName);

        Task<ApplicationEndpoint> GetApplicationEndpointAsync(Guid applicationEndpointId);

        Task<ApplicationEndpoint[]> GetApplicationEndpointsAsync(Guid applicationId);

        Task<ApplicationEndpoint[]> GetApplicationEndpointsAsync(int start, int count, string endpointName);
    }
}
