using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    public class Application : BaseEntity<Application>, IApplication
    {
        public Application() { }

        public Application(string applicationName)
        {
            ApplicationName = applicationName;
        }

        public string ApplicationName { set; get; }

        [NotMapped]
        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
