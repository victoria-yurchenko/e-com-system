using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = await _adminService.GetAllSubscriptionsAsync();
            return Ok(subscriptions);
        }

        [HttpPost("subscriptions")]
        public async Task<IActionResult> AddSubscription([FromBody] Subscription subscription)
        {
            await _adminService.AddSubscriptionAsync(subscription);
            return Ok("Subscription added successfully.");
        }

        [HttpPut("subscriptions/{id}")]
        public async Task<IActionResult> UpdateSubscription(Guid id, [FromBody] Subscription subscription)
        {
            subscription.Id = id;
            await _adminService.UpdateSubscriptionAsync(subscription);
            return Ok("Subscription updated successfully.");
        }

        [HttpDelete("subscriptions/{id}")]
        public async Task<IActionResult> DeleteSubscription(Guid id)
        {
            await _adminService.DeleteSubscriptionAsync(id);
            return Ok("Subscription deleted successfully.");
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var stats = await _adminService.GetStatisticsAsync(startDate, endDate);
            return Ok(stats);
        }
    }
}
