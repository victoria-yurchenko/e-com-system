using Application.Dispatchers;
using Microsoft.Extensions.Logging;

namespace Application.Services.Notifications
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly BaseDispatcher<NotificationDispatcher> _dispatcher;

        public NotificationService(NotificationDispatcher dispatcher, ILogger<NotificationService> logger)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
