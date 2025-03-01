namespace Application.Interfaces
{
    public interface INotificationStrategy
    {
        Task ExecuteAsync(IDictionary<string, object> parameters, IMessageSender messageSender);
    }
}