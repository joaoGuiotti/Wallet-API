using System;
using Wallet.Domain.Common;
using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Events;

public class TransactionCreatedEvent : DomainEvent
{
    public Guid TransactionId { get; set; }

    public TransactionCreatedEvent(Guid transactionId) : base()
    {
        TransactionId = transactionId;
    }
     
}
