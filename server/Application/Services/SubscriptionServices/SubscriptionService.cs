using Application.DTOs.Subscriptions;
using Application.Enums;
using Application.Interfaces.Gateways;
using Application.Interfaces.Repositories;
using Application.Interfaces.Subscriptions;
using Application.Services.Notifications;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class SubscriptionService(ISubscriptionRepository subscriptionRepository, IPaymentGateway paymentGateway, IUserSubscriptionRepository userSubscriptionRepository, NotificationService notificationService) : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository = subscriptionRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository = userSubscriptionRepository;
        private readonly IPaymentGateway _paymentGateway = paymentGateway;
        private readonly NotificationService _notificationService = notificationService;

        public async Task<IEnumerable<SubscriptionDto>> GetAllSubscriptionsAsync()
        {
            var subscriptions = await _subscriptionRepository.GetAllAsync();

            return [.. subscriptions.Select(s => new SubscriptionDto
            {
                Name = s.Name,
                Price = s.Price,
                DurationInDays = s.Duration,
                Description = s.Description
            })];
        }

        public async Task<UserSubscriptionDto> GetCurrentSubscriptionAsync(Guid userId)
        {
            var subscription = await _userSubscriptionRepository.GetUserSubscriptionAsync(userId) ?? throw new Exception("No active subscription found.");
            return new UserSubscriptionDto
            {
                SubscriptionName = subscription.Subscription.Name,
                Price = subscription.Subscription.Price,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                IsAutoRenew = subscription.IsAutoRenew
            };
        }

        public async Task<string> ProcessAutoRenewalsAsync()
        {
            var subscriptionsToRenew = await _userSubscriptionRepository.GetExpiringSubscriptionsAsync();

            foreach (var subscription in subscriptionsToRenew)
            {
                if (!subscription.IsAutoRenew)
                {
                    continue;
                }

                var paymentResult = await _paymentGateway.ProcessPaymentAsync(subscription.Subscription.Price, PaymentMethod.AutoRenewal, subscription.User.Id);

                if (!paymentResult.IsSuccess)
                {
                    // TODO Logs, notifis to email about failed attempt
                    continue;
                }

                subscription.EndDate = subscription.EndDate.AddDays(subscription.Subscription.Duration);
                subscription.SubscriptionStatus = SubscriptionStatus.AutoRenew.ToString();

                var notificationParams = new Dictionary<string, object>(){
                    {"recipient", subscription.User.Email},
                    {"deliveryMethod", "subscription.DeliveryMethod"}, // TODO set delivery method from params / email by default,
                    {"operation", Operation.Verification}
                };

                await _userSubscriptionRepository.UpdateAsync(subscription);
                await _notificationService.SendNotificationAsync(notificationParams);
            }
            return "Subscription had been renewed";
        }

        public async Task<string> ProcessSubscriptionAsync(Guid userId, SubscribeDto dto)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(dto.SubscriptionId);
            if (subscription == null)
            {
                throw new Exception("Subscription not found.");
            }

            var paymentResult = await _paymentGateway.ProcessPaymentAsync(subscription.Price, dto.PaymentMethod, userId);
            if (!paymentResult.IsSuccess)
            {
                throw new Exception("Payment failed.");
            }

            var userSubscription = await _userSubscriptionRepository.GetActiveByUserIdAsync(userId);
            if (userSubscription == null)
            {
                userSubscription = new UserSubscription
                {
                    UserId = userId,
                    SubscriptionId = dto.SubscriptionId,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddDays(subscription.Duration),
                    IsActive = true,
                    SubscriptionStatus = SubscriptionStatus.Subscription.ToString()
                };
                await _userSubscriptionRepository.AddAsync(userSubscription);
            }
            else
            {
                userSubscription.EndDate = userSubscription.EndDate > DateTime.UtcNow
                    ? userSubscription.EndDate.AddDays(subscription.Duration)
                    : DateTime.UtcNow.AddDays(subscription.Duration);

                await _userSubscriptionRepository.UpdateAsync(userSubscription);
            }

            return "Subscription successfully processed.";
        }

        public async Task<string> ChangeSubscriptionPlanAsync(Guid userId, Guid newSubscriptionId)
        {
            var userSubscription = await _userSubscriptionRepository.GetUserSubscriptionAsync(userId);
            if (userSubscription == null)
            {
                throw new Exception("No active subscription found.");
            }

            var newSubscription = await _subscriptionRepository.GetByIdAsync(newSubscriptionId);
            if (newSubscription == null)
            {
                throw new Exception("New subscription not found.");
            }

            userSubscription.SubscriptionId = newSubscriptionId;
            userSubscription.EndDate = DateTime.UtcNow.AddDays(newSubscription.Duration);

            await _userSubscriptionRepository.UpdateAsync(userSubscription);

            return "Subscription plan changed successfully.";
        }

        public async Task<string> DisableAutoRenewAsync(Guid userId)
        {
            var userSubscription = await _userSubscriptionRepository.GetUserSubscriptionAsync(userId);

            if (userSubscription == null)
            {
                throw new Exception("No active subscription found.");
            }

            userSubscription.IsAutoRenew = false;
            await _userSubscriptionRepository.UpdateAsync(userSubscription);

            return "Auto-renewal has been disabled.";
        }

        public async Task<string> CancelSubscriptionAsync(Guid userId)
        {
            var userSubscription = await _userSubscriptionRepository.GetUserSubscriptionAsync(userId);
            if (userSubscription == null)
            {
                throw new Exception("No active subscription found.");
            }

            userSubscription.IsCancelled = true;
            userSubscription.IsActive = false;

            await _userSubscriptionRepository.UpdateAsync(userSubscription);

            return "Subscription has been cancelled.";
        }

        public async Task NotifyExpiringSubscriptionsAsync()
        {
            var subscriptions = await _userSubscriptionRepository.GetExpiringSubscriptionsForNotificationAsync();

            foreach (var subscription in subscriptions)
            {
                var daysLeft = (subscription.EndDate.Date - DateTime.UtcNow.Date).Days;
                var message = daysLeft switch
                {
                    3 => "Your subscription is expiring in 3 days. Renew it now to avoid interruptions.",
                    1 => "Your subscription is expiring tomorrow. Renew it now to avoid interruptions.",
                    _ => null
                };


                // TODO: code this part of sending subscription renewal notification
                // if (message != null)
                // {
                //     await _notificationService.SendSubscriptionRenewalNotificationAsync(subscription.UserId, message);
                // }
            }
        }
    }
}
