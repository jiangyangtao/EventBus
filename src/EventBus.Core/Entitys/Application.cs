using EventBus.Abstractions.IModels;
using EventBus.Core.Base;

namespace EventBus.Core.Entitys
{
    internal class Application : BaseEntity<Application>, IApplication
    {
        internal Application() { }

        internal Application(IApplication application) : base(application)
        {
            ApplicationName = application.ApplicationName;
        }

        public string ApplicationName { set; get; }

        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
