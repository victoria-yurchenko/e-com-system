using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetFilteredTransactionsByUserIdAsync(Guid userId, string status)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId && t.PaymentStatus == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
