using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(Guid userId);
        Task<IEnumerable<Transaction>> GetFilteredTransactionsByUserIdAsync(Guid userId, string status);
        Task AddTransactionAsync(Transaction transaction);
    }
}
