﻿using EventBus.Abstractions.IModels;
using EventBus.Abstractions.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Core.Services
{
    internal class RetryQueueService : IRetryQueueService
    {
        public Task PushAsync(IRetryData retryData)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(IRetryData[] retryDatas)
        {
            throw new NotImplementedException();
        }
    }
}