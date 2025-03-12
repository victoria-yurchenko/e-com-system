using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<Subscription> GetByIdAsync(Guid id);
    }
}
