using Application.Interfaces;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly AppDbContext _context;

        public AnalyticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetSubscriptionDistributionAsync()
        {
            return await _context.UserSubscriptions
                .Include(us => us.Subscription)
                .GroupBy(us => us.Subscription.Name)
                .Select(group => new { SubscriptionName = group.Key, Count = group.Count() })
                .ToDictionaryAsync(x => x.SubscriptionName, x => x.Count);
        }

        public async Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Transactions
                .Where(t => t.CreatedAt >= startDate && t.CreatedAt <= endDate && t.PaymentStatus == "Success")
                .GroupBy(t => t.CreatedAt.ToString("yyyy-MM")) // group by month
                .Select(group => new { Month = group.Key, Revenue = group.Sum(t => t.Amount) })
                .ToDictionaryAsync(x => x.Month, x => x.Revenue);
        }
    }
}
