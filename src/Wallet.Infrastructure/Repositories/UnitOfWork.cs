using Microsoft.EntityFrameworkCore.Storage;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Shared;
using Wallet.Infrastructure.Persistence.Context;

namespace Wallet.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WalletDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly HashSet<AggregateRoot> _aggregateRoots = new();


    public UnitOfWork(WalletDbContext context)
    {
        _context = context;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_transaction is null)
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public void AddAggregateRoot(AggregateRoot aggregateRoot)
        => _aggregateRoots.Add(aggregateRoot);

    public void AddAggregateRootRange(IEnumerable<AggregateRoot> aggregateRoots)
    {
        foreach (var aggregate in aggregateRoots)
        {
            _aggregateRoots.Add(aggregate);
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        EnsureTransaction();
        await _context.SaveChangesAsync(cancellationToken);
        await _transaction!.CommitAsync(cancellationToken);
        ResetTransaction();
    }

    public async Task DoAsync(Func<IUnitOfWork, Task> workFn, CancellationToken cancellationToken)
    {
        var isAutoTrasaction = false;

        if (_transaction != null)
            await workFn(this);
        try
        {
            await StartAsync(cancellationToken);
            isAutoTrasaction = true;

            await workFn(this);

            await CommitAsync(cancellationToken);
        }
        catch (System.Exception)
        {
            if (isAutoTrasaction)
                await _transaction!.RollbackAsync(cancellationToken);
            throw;
        }
        finally
        {
            ResetTransaction();
        }
    }

    public IEnumerable<AggregateRoot> GetAggregateRoots()
      => _aggregateRoots.ToList();

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        EnsureTransaction();
        await _transaction!.RollbackAsync(cancellationToken);
        ResetTransaction();
    }

    private void ResetTransaction()
    {
        _transaction?.Dispose();
        _transaction = null;
        _aggregateRoots.Clear();
    }

    private void EnsureTransaction()
    {
        if (_transaction is null)
            throw new InvalidOperationException("No transaction started.");
    }


}
