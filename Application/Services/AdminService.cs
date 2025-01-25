using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminSubscriptionRepository _subscriptionRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public AdminService(IAdminSubscriptionRepository subscriptionRepository, IUserSubscriptionRepository userSubscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

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
