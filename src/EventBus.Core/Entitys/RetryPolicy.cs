﻿using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal class RetryPolicy : IRetryPolicy
    {
        public int IntervalTime { get; set; }

        public RetryBehavior Behavior { get; set; }
    }
}
