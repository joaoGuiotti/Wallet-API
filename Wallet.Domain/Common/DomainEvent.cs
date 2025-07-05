using System;
using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Common;

public class DomainEvent : IEvent
{
    public DateTime OccuredAt => DateTime.Now;

    public virtual object GetPayload() => this;

}
