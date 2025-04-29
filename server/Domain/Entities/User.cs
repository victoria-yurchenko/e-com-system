using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid RoleId { get; set; }
        public new string Email { get; set; } = default!;
        public new string PasswordHash { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserSubscription> UserSubscriptions { get; set; } = default!;
        public ICollection<Transaction> Transactions { get; set; } = default!;
        public ICollection<Notification> Notifications { get; set; } = default!;
    }
}
