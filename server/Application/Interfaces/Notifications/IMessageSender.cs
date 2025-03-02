using Application.Enums;

namespace Application.Interfaces.Notifications
{
    public interface IMessageSender
    {
        Task SendAsync(string recipient, MessageTemplateKey templateKey, params object[] contentParams);
    }
}