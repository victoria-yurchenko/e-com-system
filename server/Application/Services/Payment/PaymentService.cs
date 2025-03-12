using Application.DTOs.Payment;
using Application.Interfaces.Payment;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly ITransactionRepository _transactionRepository;

        public PaymentService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<TransactionDto>> GetUserTransactionsAsync(Guid userId, string? status = null)
        {
            IEnumerable<Transaction> transactions;

            if (string.IsNullOrEmpty(status))
            {
                transactions = await _transactionRepository.GetTransactionsByUserIdAsync(userId);
            }
            else
            {
                transactions = await _transactionRepository.GetFilteredTransactionsByUserIdAsync(userId, status);
            }

            return transactions.Select(t => new TransactionDto
            {
                Date = t.CreatedAt,
                Amount = t.Amount,
                Status = t.PaymentStatus,
                Provider = t.PaymentProvider
            });
        }

        public async Task RecordTransactionAsync(Guid userId, decimal amount, string status, string paymentMethod, string provider, string transactionId)
        {
            var transaction = new Transaction
            {
                UserId = userId,
                Amount = amount,
                PaymentStatus = status,
                PaymentProvider = provider,
                TransactionId = transactionId,
                PaymentMethod = paymentMethod
            };

            await _transactionRepository.AddTransactionAsync(transaction);
        }
    }
}
