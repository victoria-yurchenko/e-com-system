using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [Authorize]
        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionRepository.GetAllAsync();

            return subscriptions.Select(s => new SubscriptionDto
            {
                Name = s.Name,
                Price = s.Price,
                DurationInDays = s.Duration,
                Description = s.Description
            }).ToList();
        }

        public Task<string> ProcessSubscriptionAsync(Guid userId, SubscribeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
