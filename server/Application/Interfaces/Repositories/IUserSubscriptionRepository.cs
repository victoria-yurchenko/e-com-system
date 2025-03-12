using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IUserSubscriptionRepository
    {
        Task<UserSubscription?> GetActiveByUserIdAsync(Guid userId);
        Task<IEnumerable<UserSubscription>> GetExpiringSubscriptionsAsync();
        Task AddAsync(UserSubscription userSubscription);
        Task UpdateAsync(UserSubscription userSubscription);
        Task<UserSubscription?> GetUserSubscriptionAsync(Guid userId);
        Task<IEnumerable<UserSubscription>> GetExpiringSubscriptionsForNotificationAsync();
        Task<IEnumerable<UserSubscription>> GetSubscriptionsWithinPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
