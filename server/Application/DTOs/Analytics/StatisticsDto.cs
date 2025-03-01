namespace Application.DTOs.Analytics
{
    public record StatisticsDto
    {
        public int TotalSubscriptions { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
