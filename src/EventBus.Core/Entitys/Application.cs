using EventBus.Abstractions.Models;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class Application : BaseEntity<Application>
    {
        public Application() { }

        public string ApplicationName { set; get; }

        [NotMapped]
        public ApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
