using Microsoft.EntityFrameworkCore;
using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Persistence.Context;

namespace Wallet.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly WalletDbContext _context;
    private DbSet<Transaction> _transactions =>
        _context.Set<Transaction>();

    private readonly IUnitOfWork _unitOfWork;

    public TransactionRepository(
        WalletDbContext context,
        IUnitOfWork unitOfWork
    )
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task Create(Transaction entity, CancellationToken cancelationToken)
    {
        await _transactions.AddAsync(entity, cancelationToken);
        _unitOfWork.AddAggregateRoot(entity);
    }

    public Task Delete(Transaction entity, CancellationToken cancelationToken)
     => Task.FromResult(_transactions.Remove(entity));

    public async Task<Transaction?> Find(Guid id, CancellationToken cancelationToken)
    {
        var transaction = await _transactions
            .Include(t => t.AccountFrom)
            .Include(t => t.AccountTo)
            .FirstOrDefaultAsync(t => t.Id.Equals(id), cancelationToken);
        NotFoundException.ThrowIfNull(transaction, $"{nameof(Transaction)} {id} not found.");
        return transaction;
    }

    public Task Update(Transaction entity, CancellationToken cancelationToken)
        => Task.FromResult(_transactions.Update(entity));

}
