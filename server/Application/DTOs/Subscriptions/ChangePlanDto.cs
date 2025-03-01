namespace Application.DTOs.Subscriptions
{
    public record ChangePlanDto
    {
        public Guid NewSubscriptionId { get; set; }
    }
}
