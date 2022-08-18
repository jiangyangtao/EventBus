using EventBus.Abstractions.IModels;

namespace EventBus.Infrastructure.Entitys
{
    internal class Application : BaseEntity, IApplication
    {
        public string ApplicationName { set; get; }

        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
