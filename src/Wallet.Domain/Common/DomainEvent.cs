using System;
using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Common;

public class DomainEvent : IDomainEvent
{
    public DateTime OccuredAt => DateTime.Now;

    public object GetPayload() => this;

    protected DomainEvent()
    {
        
    }
}
