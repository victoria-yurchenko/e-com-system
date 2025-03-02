using System.Diagnostics.CodeAnalysis;
using Application.Enums;
using Application.Interfaces.Notifications;
using Application.Interfaces.Strategies;
using Action = Application.Enums.Action;

namespace Application.Strategies
{
    public class VerificationStrategy : INotificationStrategy
    {
        public async Task ExecuteAsync(IDictionary<string, object> parameters, IMessageSender messageSender)
        {
            var recipient = TryExtractValue(parameters, "recipient") as string;
            var verificationCode = TryExtractValue(parameters, "verificationCode");

            // var params = new Dictionary<string, object> { { "verificationCode", verificationCode }б  };
            await messageSender.SendAsync(recipient, MessageTemplateKey.Verification, parameters);
        }

        private object? TryExtractValue(IDictionary<string, object> parameters, string key)
        {
            if (parameters.TryGetValue($"{key}", out var value))
            {
                return value;
            }

            return null;
        }
    }
}