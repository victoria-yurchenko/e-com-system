using System;
using System.Threading.Tasks;
using Application.Configurations;
using Application.Enums;
using Application.Interfaces.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Application.Providers.Messaging
{
    public class SmsProvider : IMessageProvider
    {
        private readonly SmsConfig _smsConfig;
        private readonly ILogger<SmsProvider> _logger;
        private readonly CommonMessagesConfig _messagesConfig;


        public SmsProvider(IOptions<SmsConfig> smsOptions, IOptions<CommonMessagesConfig> messagesOptions, ILogger<SmsProvider> logger)
        {
            _smsConfig = smsOptions.Value;
            _messagesConfig = messagesOptions.Value;
            _logger = logger;


            //  TODO Twilio initialization
            TwilioClient.Init(_smsConfig.AccountSid, _smsConfig.AuthToken);
        }

        public async Task SendAsync(string recipient, string messageBody = "", params object[] contentParams)
        {
            try
            {
                var body = messageBody == string.Empty 
                    ? string.Format(_messagesConfig.Templates.Verification.Body, contentParams) 
                    : messageBody;
                
                var message = await CreateMessageResource(recipient, body);

                _logger.LogInformation($"SMS sent to {recipient}: {messageBody}, SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send SMS to {recipient}: {ex.Message}");
                throw;
            }
        }

        private async Task<MessageResource> CreateMessageResource(string recipient, string messageBody)
        {
            return await MessageResource.CreateAsync(
                to: new PhoneNumber(recipient),
                from: new PhoneNumber(_smsConfig.FromNumber),
                body: messageBody
            );
        }
    }
}
