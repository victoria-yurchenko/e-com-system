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

        public async Task ExecuteAsync(IDictionary<string, object> parameters, IMessageProvider provider)
        {
            var recipient = _parameterExtractor.TryExtractValue(parameters, "recipient") as string;
            var verificationCode = _parameterExtractor.TryExtractValue(parameters, "verificationCode") as string;
        // TODO extract from parameters the full body of the message + verification code
        // TODO: create 1 more strategy for body message content handling
            // var params = new Dictionary<string, object> { { "verificationCode", verificationCode }б  };
            await provider.SendAsync(recipient ?? string.Empty, verificationCode ?? string.Empty);
        }
    }
}