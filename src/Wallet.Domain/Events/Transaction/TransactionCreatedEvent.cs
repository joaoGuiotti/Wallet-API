using Wallet.Domain.Common;

namespace Wallet.Domain.Events.Transaction;

public sealed class TransactionCreatedEvent : DomainEvent
{
    public Guid TransactionId { get; set; }
    public Guid AccountFromId { get; set; }
    public Guid AccountToId { get; set; }

    public TransactionCreatedEvent(
        Guid transactionId,
        Guid accountFromId,
        Guid accountToId
    ) : base()
    {
        TransactionId = transactionId;
        AccountFromId = accountFromId;
        AccountToId = accountToId;
    }

    public TransactionCreatedEvent()
    { }

}
