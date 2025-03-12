using Application.DTOs.Analytics;

namespace Application.Interfaces.Analytics
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDto> GetAnalyticsAsync(DateTime startDate, DateTime endDate);
    }
}
