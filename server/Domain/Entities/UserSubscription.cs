namespace Domain.Entities
{
    public class UserSubscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsAutoRenew { get; set; }
        public bool IsCancelled { get; set; }
        public string SubscriptionStatus { get; set; }

        public User User { get; set; }
        public Subscription Subscription { get; set; }
    }
}
