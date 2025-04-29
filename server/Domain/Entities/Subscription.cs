namespace Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Duration { get; set; } // in days
        public decimal Price { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = true;


        public IEnumerable<UserSubscription> UserSubscriptions { get; set; } = default!;
    }
}
