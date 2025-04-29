using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = default!;
        public string PaymentProvider { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string TransactionId { get; set; } = default!; // Transaction ID from outer payment system
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
       
        public User User { get; set; } = default!;
    }
}
