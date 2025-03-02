using Application.Dispatchers;
using Microsoft.Extensions.Logging;
using Application.Interfaces;

namespace Application.Services.Notifications
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly BaseDispatcher<NotificationDispatcher> _dispatcher;
        private readonly IMessageSender _messageSender;

        public NotificationService(
            BaseDispatcher<NotificationDispatcher> dispatcher,
            IMessageSender messageSender,
            ILogger<NotificationService> logger)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _messageSender = messageSender;
        }

/// <summary>
/// 
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


/// </summary>
/// <param name="parameters"></param>
/// <returns></returns>
        // string recipient, Operation operation,
        public async Task SendNotificationAsync(IDictionary<string, object> parameters)
        {
            // _logger.LogInformation($"📩 Notification to User {userId}: {operation.ToString()}");

            await _dispatcher.DispatchAsync(parameters);

            // await Task.CompletedTask; // TODO for debugging
        }

        // public async Task SendBulkNotificationAsync(IEnumerable<string> notifications, string subject, string body)
        // {
        //     foreach (var email in emails)
        //     {
        //         await SendNotificationAsync(email, subject, body);
        //     }
        // }
    }
}
