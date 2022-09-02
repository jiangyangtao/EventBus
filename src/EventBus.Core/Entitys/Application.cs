﻿using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    internal class Application : BaseEntity<Application>, IApplication
    {
        public Application() { }

        public string ApplicationName { set; get; }

        [NotMapped]
        public IApplicationEndpoint[] ApplicationEndpoints { set; get; }
    }
}
