using Application.Dispatchers;
using Application.Interfaces.Notifications;
using Microsoft.Extensions.Logging;

namespace Application.Services.Notifications
{
    public class NotificationService(NotificationDispatcher dispatcher, ILogger<NotificationService> logger) : INotificationService
    {
        private readonly ILogger<NotificationService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly BaseDispatcher<NotificationDispatcher> _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));

        /*
        PARAMETERS:
        "operation" : Enums.Operation.ToString(),
        "action" : Enums.Action.ToString(),
        "recipient" : Enums.Recipient.ToString(),

        var parameters = new NotificationParams
        {
            Operation = Operation.EmailRegistrationLink,
            Action = Action.SendByEmail,
            Recipient = "user@example.com"
        };
         */

        public async Task SendNotificationAsync(IDictionary<string, object> parameters)
        {
            await _dispatcher.DispatchAsync(parameters);
        }
    }
}
