namespace Application.Interfaces.Repositories
{
    public interface IAnalyticsRepository
    {
        Task<int> GetTotalUsersAsync();
        Task<Dictionary<string, int>> GetSubscriptionDistributionAsync();
        Task<Dictionary<string, decimal>> GetMonthlyRevenueAsync(DateTime startDate, DateTime endDate);
    }
}
