namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserSubscription> UserSubscriptions { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
