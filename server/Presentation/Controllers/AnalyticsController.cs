using Application.Interfaces.Analytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/admin/analytics")]
    [Authorize(Roles = "Admin")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnalytics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var analytics = await _analyticsService.GetAnalyticsAsync(startDate, endDate);
            return Ok(analytics);
        }
    }
}
