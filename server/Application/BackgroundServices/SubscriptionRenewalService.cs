using Application.Interfaces.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.BackgroundServices
{
    public class SubscriptionRenewalService(
        IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
                    await subscriptionService.ProcessAutoRenewalsAsync();
                }

                // Check subscriptions again in 24 hours
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
                // TODO For Development use TimeSpan.FromSeconds(10) to check every 10 seconds
            }
        }
    }
}
