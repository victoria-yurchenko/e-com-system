using System.Net;
using System.Net.Mail;
using Application.Configurations;
using Application.Enums;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Providers.Messaging
{
    public class EmailProvider : IMessageSender
    {
        private readonly SmtpConfig _smtpConfig;
        private readonly CommonMessagesConfig _messagesConfig;
        private readonly ILogger<EmailProvider> _logger;

        public EmailProvider(IOptions<SmtpConfig> smtpOptions, IOptions<CommonMessagesConfig> messagesOptions, ILogger<EmailProvider> logger)
        {
            _smtpConfig = smtpOptions.Value;
            _messagesConfig = messagesOptions.Value;
            _logger = logger;
        }

        public async Task SendAsync(string recipient, MessageTemplateKey templateKey, params object[] contentParams)
        {
            if (!_messagesConfig.Email.Templates.TryGetValue(templateKey.ToString(), out var emailTemplate))
            {
                _logger.LogError($"Email template '{templateKey}' not found.");
                throw new ArgumentException($"Template '{templateKey}' not found.");
            }

            var emailBody = string.Format(emailTemplate.Body, contentParams);

            // _logger.LogInformation($"Sending email to {recipient}:");
            // _logger.LogInformation($"Subject: {emailSubject}");
            // _logger.LogInformation($"Body: {emailBody}");

            using var mailMessage = CreateMailMessage(emailTemplate.Subject, emailBody, recipient);
            using var smtpClient = CreateSmtpClient();

            await smtpClient.SendMailAsync(mailMessage);
            //            await Task.CompletedTask; // TODO remove after debug, add real logic
        }

        private MailMessage CreateMailMessage(string emailSubject, string emailBody, string recipient)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpConfig.FromEmail, "Giloggi"), // TODO: move name of the sender to the config file
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = true // TODO: change to false if plain text is needed
            };

            mailMessage.To.Add(recipient);
            return mailMessage;
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient(_smtpConfig.Server, _smtpConfig.Port)
            {
                Credentials = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password),
                EnableSsl = _smtpConfig.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000
            };
        }
    }
}