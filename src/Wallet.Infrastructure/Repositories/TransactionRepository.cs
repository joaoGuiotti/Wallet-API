using Microsoft.EntityFrameworkCore;
using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Repository;
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
            .AsSplitQuery()
            .FirstOrDefaultAsync(t => t.Id.Equals(id), cancelationToken);
        NotFoundException.ThrowIfNull(transaction, $"{nameof(Transaction)} {id} not found.");
        return transaction;
    }

    public Task Update(Transaction entity, CancellationToken cancelationToken)
        => Task.FromResult(_transactions.Update(entity));

    public async Task<SearchOutput<Transaction>> SearchAsync(SearchInput input, CancellationToken cancellationToken)
    {
        var query = _transactions
             .Include(t => t.AccountFrom)
             .Include(t => t.AccountTo)
             .AsSplitQuery()
             .AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((input.Page - 1) * input.PerPage)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken);
        return new SearchOutput<Transaction>(
            input.Page,
            input.PerPage,
            totalCount,
            items.AsReadOnly()
        );
    }

    private IQueryable<Transaction> AddOrderToQuery(IQueryable<Transaction> query, string orderBy, ESearchOrder order)
    {
        return (orderBy.ToLower(), order) switch
        {
            ("id", ESearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", ESearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("accountfromid", ESearchOrder.Asc) => query.OrderBy(x => x.AccountFromId)
                .ThenBy(x => x.Id),
            ("accountfromid", ESearchOrder.Desc) => query.OrderByDescending(x => x.AccountFromId)
                .ThenBy(x => x.Id),
            ("accounttoid", ESearchOrder.Asc) => query.OrderBy(x => x.AccountToId)
                .ThenBy(x => x.Id),
            ("accounttoid", ESearchOrder.Desc) => query.OrderByDescending(x => x.AccountToId)
                .ThenBy(x => x.Id),
            _ => query.OrderByDescending(x => x.CreatedAt)
        };
    }
}
