using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IProviders
{
    public interface IRetryManager
    {
        Task RetryAsync(string retryDataId);
    }
}
