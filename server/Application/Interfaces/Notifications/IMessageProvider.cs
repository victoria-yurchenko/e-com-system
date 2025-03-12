namespace Application.Interfaces.Notifications
{
    public interface IMessageProvider
    {
        Task SendAsync(string recipient, string messageBody = "", params object[] contentParams);
    }
}