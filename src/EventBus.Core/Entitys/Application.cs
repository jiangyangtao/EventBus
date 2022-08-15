using EventBus.Domain.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class Application : BaseEntity, IApplication
    {
        public string ApplicationName { set; get; }

        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
