using Application.Interfaces.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IPaymentService _transactionService;

        public TransactionController(IPaymentService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] string? status = null)
        {
            var userId = Guid.Parse(User.FindFirst("sub")?.Value ?? string.Empty);
            var transactions = await _transactionService.GetUserTransactionsAsync(userId, status);
            return Ok(transactions);
        }
    }
}