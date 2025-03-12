using Application.Enums;
using Application.Interfaces.Notifications;
using Application.Interfaces.Factories;
using Application.Interfaces.Strategies;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Utils;

namespace Application.Dispatchers
{
    public sealed class NotificationDispatcher(
        IStrategiesFactory<INotificationStrategy, Operation> strategiesFactory,
        IFactory<IMessageProvider, DeliveryMethod> messagingProviderFactory,
        ILogger<NotificationDispatcher> logger,
        IParameterExtractorService parameterExtractor) : BaseDispatcher<NotificationDispatcher>(logger)
    {
        private readonly IStrategiesFactory<INotificationStrategy, Operation> _strategiesFactory = strategiesFactory;
        private readonly IFactory<IMessageProvider, DeliveryMethod> _messagingProviderFactory = messagingProviderFactory;
        private readonly IParameterExtractorService _parameterExtractor = parameterExtractor;

        public override async Task DispatchAsync(IDictionary<string, object> parameters)
        {
            var operation = _parameterExtractor.GetEnumParameter<Operation>(parameters, "operation");
            var deliveryMethod = _parameterExtractor.GetEnumParameter<DeliveryMethod>(parameters, "deliveryMethod");
            var provider = _messagingProviderFactory.Create(deliveryMethod);
            var strategy = _strategiesFactory.CreateStrategy(operation);
            // _logger.LogInformation($"📩 Dispatching notification: {operation}");
            await strategy.ExecuteAsync(parameters, provider);
        }
    }
}
