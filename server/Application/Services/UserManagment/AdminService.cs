using Application.DTOs.Analytics;
using Application.Interfaces.Repositories;
using Application.Interfaces.UserManagment;
using Domain.Entities;

namespace Application.Services
{
    public class AdminService(
        IAdminSubscriptionRepository subscriptionRepository, 
        IUserSubscriptionRepository userSubscriptionRepository) : IAdminService
    {
        private readonly IAdminSubscriptionRepository _subscriptionRepository = subscriptionRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository = userSubscriptionRepository;

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _subscriptionRepository.GetAllSubscriptionsAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(Guid subscriptionId)
        {
            return await _subscriptionRepository.GetSubscriptionByIdAsync(subscriptionId);
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _subscriptionRepository.AddSubscriptionAsync(subscription);
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            await _subscriptionRepository.UpdateSubscriptionAsync(subscription);
        }

        public async Task DeleteSubscriptionAsync(Guid subscriptionId)
        {
            await _subscriptionRepository.DeleteSubscriptionAsync(subscriptionId);
        }

        public async Task<StatisticsDto> GetStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var subscriptions = await _userSubscriptionRepository.GetSubscriptionsWithinPeriodAsync(startDate, endDate);

            return new StatisticsDto
            {
                TotalSubscriptions = subscriptions.Count(),
                TotalRevenue = subscriptions.Sum(s => s.Subscription.Price)
            };
        }
    }
}
