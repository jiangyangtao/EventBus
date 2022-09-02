using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class Application : BaseEntity<Application>, Abstractions.IModels.Application
    {
        public Application() { }

        public string ApplicationName { set; get; }

        [NotMapped]
        public Abstractions.IModels.ApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
