namespace Application.DTOs.Payment
{
    public record TransactionDto
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Provider { get; set; }
    }
}
