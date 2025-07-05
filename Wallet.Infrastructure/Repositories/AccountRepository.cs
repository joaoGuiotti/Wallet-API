using Microsoft.EntityFrameworkCore;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Domain.Shared;
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
    }
}
