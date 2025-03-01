using Application.Interfaces;
using ActionType = Application.Enums.Action;
using Microsoft.Extensions.DependencyInjection;
using Application.Providers.Messaging;

namespace Application.Factories
{
    public class MessageSenderFactory : IFactory<IMessageSender, ActionType>
    {
        private readonly IServiceProvider _serviceProvider;

        public MessageSenderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMessageSender Create(ActionType operation)
        {
            return operation switch
            {
                ActionType.SendByEmail => _serviceProvider.GetRequiredService<EmailProvider>(),
                _ => throw new ArgumentException($"Unknown message sender type: {operation}")
            };
        }
    }
}
