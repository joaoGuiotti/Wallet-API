
using Wallet.Application.Kafka;
using Wallet.Domain.Events.Transaction;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Events.Transaction;

public class TransactionCreatedEventHandler : IDomainEventHandler<TransactionCreatedEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public TransactionCreatedEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }

    public async Task Handle(TransactionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new TransactionCreatedIntegrationEvent(
            domainEvent.TransactionId,
            domainEvent.AccountFromId,
            domainEvent.AccountToId
        );
        await _integrationEventPublisher.PublishAsync(integrationEvent);
    }
}
