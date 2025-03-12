using Application.Interfaces.Notifications;

namespace Application.Interfaces.Strategies
{
    public interface INotificationStrategy
    {
        Task ExecuteAsync(IDictionary<string, object> parameters, IMessageProvider provider);
    }
}