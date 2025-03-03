using Application.Interfaces.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Application.Providers.Messaging;
using Application.Interfaces.Factories;
using Action = Application.Enums.Action;

namespace Application.Factories
{
    public class MessageSenderFactory : IFactory<IMessageSender, Action>
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageSenderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMessageSender Create(Action operation)
        {
            return operation switch
            {
                Action.SendByEmail => _serviceProvider.GetRequiredService<EmailProvider>(),
                _ => throw new ArgumentException($"Unknown message sender type: {operation}")
            };
        }
    }
}
