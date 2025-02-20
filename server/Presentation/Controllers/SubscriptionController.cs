using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/subscriptions")]
    [Authorize]
    public class SubscriptionController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubscriptions()
        {
            var subscriptions = await _subscriptionService.GetAllSubscriptionsAsync();
            return Ok(subscriptions);
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscribeDto dto)
        {
            // Get user ID through JWT-token
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);

            var result = await _subscriptionService.ProcessSubscriptionAsync(userId, dto);
            return Ok(result);
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentSubscription()
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var subscription = await _subscriptionService.GetCurrentSubscriptionAsync(userId);
            return Ok(subscription);
        }

        [HttpPost("change-plan")]
        public async Task<IActionResult> ChangeSubscriptionPlan([FromBody] ChangePlanDto dto)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var result = await _subscriptionService.ChangeSubscriptionPlanAsync(userId, dto.NewSubscriptionId);
            return Ok(result);
        }

        [HttpPost("disable-auto-renew")]
        public async Task<IActionResult> DisableAutoRenew()
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var result = await _subscriptionService.DisableAutoRenewAsync(userId);
            return Ok(result);
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSubscription()
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value);
            var result = await _subscriptionService.CancelSubscriptionAsync(userId);
            return Ok(result);
        }
    }
}
