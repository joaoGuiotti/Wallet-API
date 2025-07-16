using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Domain.Repository;
using Wallet.Infrastructure.Persistence.Context;

namespace Wallet.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly WalletDbContext _context;

        public AccountRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task Create(Account entity, CancellationToken cancellationToken)
        {
            await _context.Accounts.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Account entity, CancellationToken cancelationToken)
             => await Task.FromResult(_context.Accounts.Remove(entity));

        public async Task<Account?> Find(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.Accounts.Include(a => a.Client)
                .FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Account>> FindAll()
        {
            return await _context.Accounts.Include(a => a.Client)
                .ToListAsync();
        }

        public async Task Update(Account entity, CancellationToken cancellationToken)
            => await Task.FromResult(_context.Accounts.Update(entity));

        public async Task<SearchOutput<Account>> SearchAsync(
            SearchInput input,
            CancellationToken cancellationToken
        )
        {
            var query = _context.Accounts.Include(x => x.Client).AsNoTracking();
            query = AddOrderToQuery(query, input.OrderBy, input.Order);
            if (!String.IsNullOrWhiteSpace(input.SearchTerm))
                query = query.Where(x => x.Id.ToString().Contains(input.SearchTerm));
            var items = await query
                .Skip((input.Page - 1) * input.PerPage)
                .Take(input.PerPage)
                .ToListAsync();
            var count = await query.CountAsync();
            return new SearchOutput<Account>(
                input.Page,
                input.PerPage,
                count,
                items.AsReadOnly()
            );
        }

        private IQueryable<Account> AddOrderToQuery(IQueryable<Account> query, string orderProp, ESearchOrder order)
            => (orderProp.ToLower(), order) switch
            {
                ("id", ESearchOrder.Asc) => query.OrderBy(x => x.Id),
                ("id", ESearchOrder.Desc) => query.OrderByDescending(x => x.Id),
                ("clientid", ESearchOrder.Asc) => query.OrderBy(x => x.ClientId),
                ("clientid", ESearchOrder.Desc) => query.OrderByDescending(x => x.ClientId),
                ("createdat", ESearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", ESearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderByDescending(x => x.CreatedAt)
                    .ThenBy(x => x.Id)
            };

    }
}
