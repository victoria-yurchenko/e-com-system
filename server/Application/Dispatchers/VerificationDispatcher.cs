using Application.Enums;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Application.Interfaces.Notifications;
using Application.Interfaces.Strategies;
using Microsoft.Extensions.Logging;
using Action = Application.Enums.Action;

namespace Application.Dispatchers
{
    public class VerificationDispatcher(
        IFactory<INotificationStrategy, Operation> strategiesFactory,
        IFactory<IMessageSender, Action> messageSenderFactory,
        ILogger<VerificationDispatcher> logger) : BaseDispatcher<VerificationDispatcher>(logger)
    {
        private readonly IFactory<INotificationStrategy, Operation> _strategiesFactory = strategiesFactory;
        private readonly IFactory<IMessageSender, Action> _messageSenderFactory = messageSenderFactory;

        public override async Task DispatchAsync(IDictionary<string, object> parameters)
        {
            var operation = GetParameter<Operation>(parameters, "operation");
            var action = GetParameter<Action>(parameters, "action");

            var messageSender = _messageSenderFactory.Create(action);
            var strategy = _strategiesFactory.Create(operation);
            await strategy.ExecuteAsync(parameters, messageSender);
        }
    }
}
