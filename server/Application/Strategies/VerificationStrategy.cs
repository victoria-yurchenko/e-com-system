using Application.Enums;
using Application.Interfaces.Notifications;
using Application.Interfaces.Strategies;
using Application.Interfaces.Utils;

namespace Application.Strategies
{
    public class VerificationStrategy(
        IParameterExtractorService parameterExtractor
    ) : INotificationStrategy
    {
        private readonly IParameterExtractorService _parameterExtractor = parameterExtractor;

        public async Task ExecuteAsync(IDictionary<string, object> parameters, IMessageSender messageSender)
        {
            var recipient = _parameterExtractor.TryExtractValue(parameters, "recipient") as string;
            var verificationCode = _parameterExtractor.TryExtractValue(parameters, "verificationCode") as string;

            // var params = new Dictionary<string, object> { { "verificationCode", verificationCode }б  };
            await messageSender.SendAsync(recipient ?? string.Empty, MessageTemplateKey.Verification, verificationCode ?? string.Empty);
        }
    }
}