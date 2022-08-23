using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class Application : BaseEntity<Application>, IApplication
    {
        public string ApplicationName { set; get; }

        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
