using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminSubscriptionRepository : IAdminSubscriptionRepository
    {
        private readonly AppDbContext _context;

        public AdminSubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(Guid subscriptionId)
        {
            return await _context.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionAsync(Guid subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
