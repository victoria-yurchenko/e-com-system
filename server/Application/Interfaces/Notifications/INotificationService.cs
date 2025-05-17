namespace Application.Interfaces.Notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(IDictionary<string, object> parameters);
        // Task SendBulkNotificationAsync(IEnumerable<string> recipients, IDictionary<string, object> parameters)
    }
}