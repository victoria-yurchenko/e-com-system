using Application.Enums;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;
using Application.Strategies;

namespace Application.Factories
{
    public class NotificationFactory : IFactory<INotificationStrategy, Operation>
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INotificationStrategy Create(Operation operation)
        {
            return operation switch
            {
                Operation.Verification => _serviceProvider.GetRequiredService<VerificationStrategy>(),
                // TODO: add other strategies
                _ => throw new ArgumentException($"Unknown operation: {operation}")
            };
        }
    }
}
