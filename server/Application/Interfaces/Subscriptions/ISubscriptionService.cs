using Application.DTOs.Subscriptions;

namespace Application.Interfaces.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync();
        Task<string> ProcessSubscriptionAsync(Guid userId, SubscribeDto dto);
        Task<string> ProcessAutoRenewalsAsync();
        Task<UserSubscriptionDto> GetCurrentSubscriptionAsync(Guid userId);
        Task<string> ChangeSubscriptionPlanAsync(Guid userId, Guid newSubscriptionId);
        Task<string> DisableAutoRenewAsync(Guid userId);
        Task<string> CancelSubscriptionAsync(Guid userId);
        Task NotifyExpiringSubscriptionsAsync();
    }
}
