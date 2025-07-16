using MediatR;

namespace Wallet.Domain.Interfaces
{
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    { }
}
