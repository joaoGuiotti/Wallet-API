using Wallet.Domain.Shared;

namespace Wallet.Domain.Interfaces;

public interface IUnitOfWork
{
    Task StartAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task DoAsync(Func<IUnitOfWork, Task> workFn, CancellationToken cancellationToken);
    void AddAggregateRoot(AggregateRoot aggregateRoot);
    void AddAggregateRootRange(IEnumerable<AggregateRoot> aggregateRoots);
    IEnumerable<AggregateRoot> GetAggregateRoots();
}
