using Domain.Enums;

namespace Application.Interfaces.Gateways
{
    public interface IPaymentGateway
    {
        Task<PaymentResult> ProcessPaymentAsync(decimal amount, PaymentMethod paymentMethod, Guid userId);
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; }
        public string PaymentUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
