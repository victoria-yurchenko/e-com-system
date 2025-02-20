using Application.Interfaces;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly AppDbContext _context;

        public UserSubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserSubscription userSubscription)
        {
            await _context.UserSubscriptions.AddAsync(userSubscription);
            await _context.SaveChangesAsync();
        }

        public async Task<UserSubscription> GetActiveByUserIdAsync(Guid userId)
        {
            return await _context.UserSubscriptions.FirstOrDefaultAsync(us => us.UserId == userId && us.IsActive && us.EndDate > DateTime.UtcNow);
        }

        public async Task<IEnumerable<UserSubscription>> GetExpiringSubscriptionsAsync()
        {
            return await _context.UserSubscriptions
                .Include(us => us.Subscription)
                .Where(us => us.IsActive && us.IsAutoRenew && us.EndDate <= DateTime.UtcNow.AddDays(3))
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSubscription>> GetExpiringSubscriptionsForNotificationAsync()
        {
            var currentDate = DateTime.UtcNow;
            return await _context.UserSubscriptions
                .Include(us => us.User)
                .Where(us => us.IsActive && !us.IsCancelled &&
                             (us.EndDate.Date == currentDate.AddDays(3).Date || us.EndDate.Date == currentDate.AddDays(1).Date))
                .ToListAsync(); 
        }

        public async Task<IEnumerable<UserSubscription>> GetSubscriptionsWithinPeriodAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.UserSubscriptions.Where(us => us.StartDate >= startDate && us.EndDate <= endDate).ToListAsync();
        }

        public async Task<UserSubscription> GetUserSubscriptionAsync(Guid userId)
        {
            return await _context.UserSubscriptions.Where(us => us.UserId == userId && us.IsActive).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(UserSubscription userSubscription)
        {
            _context.UserSubscriptions.Update(userSubscription);
            await _context.SaveChangesAsync();
        }
    }
}
