using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Shared
{
    public interface IAggreagateRoot
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }
        void ApplyEvent(IDomainEvent @event);
        void ClearEvents();
        IReadOnlyCollection<IDomainEvent> PopEvents();
    }

    public class AggregateRoot : Entity , IAggreagateRoot
    {
        private readonly HashSet<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> Events
            => _events.ToList().AsReadOnly();

        protected AggregateRoot() : base() { }

        public void ApplyEvent(IDomainEvent @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event), "Event cannot be null.");
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        public IReadOnlyCollection<IDomainEvent> PopEvents()
        {
            var events = _events.ToList();
            _events.Clear();
            return events.AsReadOnly();
        }
    }
}
