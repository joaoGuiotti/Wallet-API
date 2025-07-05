namespace Wallet.Domain.Shared
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public Notification Notification { get; private set; }

        protected Entity()
        {
            Notification = new Notification();
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.Now;
        }
    }
}
