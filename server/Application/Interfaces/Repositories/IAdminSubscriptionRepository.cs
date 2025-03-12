using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAdminSubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(Guid subscriptionId);
        Task AddSubscriptionAsync(Subscription subscription);
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(Guid subscriptionId);
    }
}
