using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(Guid subscriptionId);
        Task AddSubscriptionAsync(Subscription subscription);
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(Guid subscriptionId);
        Task<StatisticsDto> GetStatisticsAsync(DateTime startDate, DateTime endDate);
    }
}
