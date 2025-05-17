using System.Net;
using System.Net.Mail;
using Application.Configurations;
using Application.Interfaces.Notifications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Providers.Messaging
{
    public class EmailProvider(
        IOptions<SmtpConfig> smtpOptions,
        IOptions<CommonMessagesConfig> messagesOptions,
        ILogger<EmailProvider> logger) : IMessageProvider
    {
        private readonly SmtpConfig _smtpConfig = smtpOptions.Value;
        private readonly CommonMessagesConfig _messagesConfig = messagesOptions.Value;
        private readonly ILogger<EmailProvider> _logger = logger;

        public async Task SendAsync(string recipient, string messageBody = "", params object[] contentParams)
        {
            var emailTemplate = _messagesConfig.Templates.Verification;
            var body = messageBody == string.Empty ? string.Format(emailTemplate.Body, contentParams) : messageBody;
            var subject = emailTemplate.Subject;

            _logger.LogInformation($"SMTP Username: {_smtpConfig.Username}");
            _logger.LogInformation($"SMTP Password: {_smtpConfig.Password}");
            _logger.LogInformation($"SMTP Server: {_smtpConfig.Server}");
            _logger.LogInformation($"SMTP Port: {_smtpConfig.Port}");
            _logger.LogInformation($"SMTP EnableSsl: {_smtpConfig.EnableSsl}");


            _logger.LogInformation($"Sending email to {recipient}:");
            _logger.LogInformation($"Subject: {subject}");
            _logger.LogInformation($"emailTemplate: {emailTemplate}");
            _logger.LogInformation($"Body0: {body}");

            using var mailMessage = CreateMailMessage(subject, body, recipient);
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