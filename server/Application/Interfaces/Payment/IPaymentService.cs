﻿using Application.DTOs.Payment;

namespace Application.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<IEnumerable<TransactionDto>> GetUserTransactionsAsync(Guid userId, string? status = null);
        Task RecordTransactionAsync(Guid userId, decimal amount, string status, string paymentMethod, string provider, string transactionId);
    }
}
