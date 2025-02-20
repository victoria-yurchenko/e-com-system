using Domain.Enums;

namespace Application.DTOs
{
    public class SubscribeDto
    {
        public Guid SubscriptionId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
