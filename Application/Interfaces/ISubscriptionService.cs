using Application.DTOs;

namespace Application.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();

        Task<string> ProcessSubscriptionAsync(Guid userId, SubscribeDto dto);
    }
}
