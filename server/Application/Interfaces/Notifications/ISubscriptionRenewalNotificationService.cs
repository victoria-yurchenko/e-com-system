namespace Application.Interfaces
{
    public interface ISubscriptionRenewalNotificationService
    {
        Task SendSubscriptionRenewalNotificationAsync(Guid userId, string message);
        Task SendBulkSubscriptionRenewalNotificationAsync(IEnumerable<Guid> userId, string message);
    }
}
