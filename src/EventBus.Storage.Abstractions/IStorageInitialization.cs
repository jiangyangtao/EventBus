﻿using EventBus.Storage.Abstractions.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Storage.Abstractions
{
    public interface IStorageInitialization
    {
        void Initialize(IServiceCollection services, string connectionString);
    }
}
