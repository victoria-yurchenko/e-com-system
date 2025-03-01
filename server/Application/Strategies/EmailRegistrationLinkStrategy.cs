using Application.Enums;
using Application.Interfaces;

namespace Application.Strategies
{
    public class EmailRegistrationLinkStrategy : INotificationStrategy
    {
        public async Task ExecuteAsync(IDictionary<string, object> parameters, IMessageSender messageSender)
        {
            if (!parameters.TryGetValue("recipient", out var recipientObj) || recipientObj is not string recipient)
                throw new ArgumentException("Missing or invalid recipient.");

            parameters["verificationCode"] = GenerateVerificationCode();
            // var params = new Dictionary<string, object> { { "verificationCode", verificationCode }б  };
            await messageSender.SendAsync(recipient, MessageTemplateKey.Registration, parameters);
        }

        private string GenerateVerificationCode(int length = 10)
        {
            return new string([.. Enumerable.Range(0, length).Select(_ => (char)('0' + Random.Shared.Next(10)))]);
        }
    }
}