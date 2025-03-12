using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetByIdAsync(Guid id)
        {
            return await _context.Subscriptions.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
    }
}
