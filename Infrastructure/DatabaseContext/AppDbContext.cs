using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // UserSubscription
            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSubscriptions)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(us => us.Subscription)
                .WithMany(s => s.UserSubscriptions)
                .HasForeignKey(us => us.SubscriptionId);

            // Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(18,2);

            // Notification
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // Subscription
            modelBuilder.Entity<Subscription>()
                .Property(s => s.Price)
                .HasPrecision(18, 2);
        }
    }
}
