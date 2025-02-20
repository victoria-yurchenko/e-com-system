using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAnalyticsService
    {
        Task<AnalyticsDto> GetAnalyticsAsync(DateTime startDate, DateTime endDate);
    }
}
