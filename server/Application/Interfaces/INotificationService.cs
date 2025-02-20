namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(Guid userId, string message);
        Task SendSubscriptionRenewalNotificationAsync();
        Task SendSubscriptionRenewalNotificationAsync(Guid userId, string message);
    }
}
