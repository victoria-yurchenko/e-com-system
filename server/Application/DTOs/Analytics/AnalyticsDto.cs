namespace Application.DTOs.Analytics
{
    public record AnalyticsDto
    {
        public int TotalUsers { get; set; }
        public Dictionary<string, int> SubscriptionDistribution { get; set; } = [];
        public Dictionary<string, decimal> MonthlyRevenue { get; set; } = [];
    }
}
