using Domain.Enums;

namespace Application.DTOs.Subscriptions
{
    public record SubscribeDto
    {
        public Guid SubscriptionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
