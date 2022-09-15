using EventBus.Abstractions.IProviders;
using Microsoft.Extensions.Hosting;

namespace EventBus.Core.Services
{
    /// <summary>
    /// 重试计划
    /// </summary>
    internal class RetrySchedulesService : IHostedService, IDisposable
    {
        private readonly System.Timers.Timer Timer;
        private readonly TimeSpan Interval = TimeSpan.FromSeconds(1);
        private readonly IRetryProvider _retryProvider;


        protected RetrySchedulesService(IRetryProvider retryProvider)
        {
            Timer = new System.Timers.Timer(Interval.TotalMilliseconds);
            Timer.Elapsed += async (sender, e) =>
            {
                await ExecuteingAsync();
            };

            _retryProvider = retryProvider;
        }

        public void Dispose() => Timer.Dispose();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (Timer != null) Timer.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (Timer != null) Timer.Stop();

            return Task.CompletedTask;
        }

        private async Task ExecuteingAsync()
        {
            await _retryProvider.RetryAsync();
        }
    }
}
