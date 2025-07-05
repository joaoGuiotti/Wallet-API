using Microsoft.EntityFrameworkCore;
using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Infrastructure.Persistence.Context;

namespace Wallet.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly WalletDbContext _context;

        public ClientRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task Create(Client entity, CancellationToken cancellationToken)
            => await _context.Clients.AddAsync(entity, cancellationToken);

        public Task Delete(Client entity, CancellationToken cancelationToken)
            => Task.FromResult(_context.Clients.Remove(entity));

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Clients.AnyAsync(c => c.Email == email, cancellationToken);

        public async Task<Client?> Find(Guid id, CancellationToken cancellationToken)
        {
            var client = await _context.Clients
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            NotFoundException.ThrowIfNull(client, $"{nameof(Client)} '{id}' not found.");
            return client;
        }

        public Task Update(Client entity, CancellationToken cancellationToken)
            => Task.FromResult(_context.Clients.Update(entity));
    }
}
