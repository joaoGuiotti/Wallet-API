using System;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Interfaces.Events;

public interface IDomainDispatcher
{
    Task DispatchAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken);
}
