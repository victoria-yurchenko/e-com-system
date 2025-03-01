namespace Application.DTOs.Subscriptions
{
    public record SubscriptionDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public string Description { get; set; }
    }
}
