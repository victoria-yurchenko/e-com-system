namespace Application.DTOs
{
    public class UserSubscriptionDto
    {
        public string SubscriptionName { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsAutoRenew { get; set; }
    }
}
