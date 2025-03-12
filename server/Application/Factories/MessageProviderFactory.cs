using Application.Interfaces.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Application.Providers.Messaging;
using Application.Interfaces.Factories;
using Application.Enums;

namespace Application.Factories
{
    public class MessagingProviderFactory(IServiceProvider serviceProvider) : IFactory<IMessageProvider, DeliveryMethod>
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public IMessageProvider Create(DeliveryMethod deliveryMethod)
        {
            return deliveryMethod switch
            {
                DeliveryMethod.Email => _serviceProvider.GetRequiredService<EmailProvider>(),
                DeliveryMethod.Sms => _serviceProvider.GetRequiredService<SmsProvider>(),
                // DeliveryMethod.GoogleAuthentication => _serviceProvider.GetRequiredService<GoogleAuthProvider>(),
                _ => throw new ArgumentException($"Unknown message sender type: {deliveryMethod}")
            };
        }
    }
}
