using Application.Interfaces;
using Application.Enums;
using Application.Strategies;
using Microsoft.Extensions.DependencyInjection;

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
                Operation.EmailRegistrationLink => _serviceProvider.GetRequiredService<EmailRegistrationLinkStrategy>(),
                _ => throw new ArgumentException($"Unknown operation: {operation}")
            };
        }
    }
}
