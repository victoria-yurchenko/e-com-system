using Application.Enums;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;

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
                Operation.RegistrationActivationLink => _serviceProvider.GetRequiredService<EmailRegistrationLinkStrategy>(),
                _ => throw new ArgumentException($"Unknown operation: {operation}")
            };
        }
    }
}
