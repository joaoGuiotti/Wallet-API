using System;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Interfaces.Events;

public interface IDomainDispatcher
{
    Task DispatchAsync(string topic, IEnumerable<IDomainEvent> events, CancellationToken cancellationToken);
}
