using Application.Enums;
using Application.Interfaces.Notifications;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;
using Microsoft.Extensions.Logging;
using Action = Application.Enums.Action;
using Application.Interfaces.Utils;

namespace Application.Dispatchers
{
    public class NotificationDispatcher(
        IStrategiesFactory<INotificationStrategy, Operation> strategiesFactory,
        IFactory<IMessageSender, Action> messageSenderFactory,
        ILogger<NotificationDispatcher> logger,
        IParameterExtractorService parameterExtractor) : BaseDispatcher<NotificationDispatcher>(logger)
    {
        private readonly IStrategiesFactory<INotificationStrategy, Operation> _strategiesFactory = strategiesFactory;
        private readonly IFactory<IMessageSender, Action> _messageSenderFactory = messageSenderFactory;
        private readonly IParameterExtractorService _parameterExtractor = parameterExtractor;

        public override async Task DispatchAsync(IDictionary<string, object> parameters)
        {
            var operation = _parameterExtractor.GetEnumParameter<Operation>(parameters, "operation");
            var action = _parameterExtractor.GetEnumParameter<Action>(parameters, "action");
            // _logger.LogInformation($"📩 Dispatching notification: {operation}");

            var messageSender = _messageSenderFactory.Create(action);
            var strategy = _strategiesFactory.CreateStrategy(operation);
            await strategy.ExecuteAsync(parameters, messageSender);
        }
    }
}
