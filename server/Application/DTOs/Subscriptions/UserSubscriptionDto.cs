namespace Application.DTOs.Subscriptions
{
    public record UserSubscriptionDto
    {
        public string SubscriptionName { get; set; } = default!;
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAutoRenew { get; set; }
    }
}
