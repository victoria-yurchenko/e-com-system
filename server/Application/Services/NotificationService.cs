using Application.Interfaces;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        public Task SendNotificationAsync(Guid userId, string message)
        {
            // Реализация отправки уведомлений (например, email, SMS, push-уведомления)
            Console.WriteLine($"Notification to User {userId}: {message}");
            return Task.CompletedTask;
        }

        public Task SendSubscriptionRenewalNotificationAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendSubscriptionRenewalNotificationAsync(Guid userId, string message)
        {
            throw new NotImplementedException();
        }
    }
}
