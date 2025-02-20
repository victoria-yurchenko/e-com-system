using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;

        public AnalyticsService(IAnalyticsRepository analyticsRepository)
        {
            _analyticsRepository = analyticsRepository;
        }

        public async Task<AnalyticsDto> GetAnalyticsAsync(DateTime startDate, DateTime endDate)
        {
            var totalUsers = await _analyticsRepository.GetTotalUsersAsync();
            var subscriptionDistribution = await _analyticsRepository.GetSubscriptionDistributionAsync();
            var monthlyRevenue = await _analyticsRepository.GetMonthlyRevenueAsync(startDate, endDate);

            return new AnalyticsDto
            {
                TotalUsers = totalUsers,
                SubscriptionDistribution = subscriptionDistribution,
                MonthlyRevenue = monthlyRevenue
            };
        }
    }
}
