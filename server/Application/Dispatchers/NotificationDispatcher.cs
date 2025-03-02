using Application.Enums;
using Application.Interfaces.Notifications;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;
using Microsoft.Extensions.Logging;
using Action = Application.Enums.Action;

namespace Application.Dispatchers
{
    public class NotificationDispatcher : BaseDispatcher<NotificationDispatcher>
    {
        private readonly IStrategiesFactory<INotificationStrategy, Operation> _strategiesFactory;
        private readonly IFactory<IMessageSender, Action> _messageSenderFactory;

        public NotificationDispatcher(
            IStrategiesFactory<INotificationStrategy, Operation> strategiesFactory,
            IFactory<IMessageSender, Action> messageSenderFactory,
            ILogger<NotificationDispatcher> logger)
            : base(logger)
        {
            _strategiesFactory = strategiesFactory;
            _messageSenderFactory = messageSenderFactory;
        }

        public override async Task DispatchAsync(IDictionary<string, object> parameters)
        {
            var operation = GetParameter<Operation>(parameters, "operation");
            var action = GetParameter<Action>(parameters, "action");

            // _logger.LogInformation($"📩 Dispatching notification: {operation}");

            var messageSender = _messageSenderFactory.Create(action);
            var strategy = _strategiesFactory.CreateStrategy(operation);
            await strategy.ExecuteAsync(parameters, messageSender);
        }
    }
}
