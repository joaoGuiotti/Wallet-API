using Microsoft.EntityFrameworkCore;
using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Domain.Entities;
using Wallet.Domain.Repository;
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
       
        public async Task<SearchOutput<Client>> SearchAsync(SearchInput input, CancellationToken cancellationToken)
        {
           var query = _context.Clients.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(input.SearchTerm) || c.Email.Contains(input.SearchTerm));
            }
            query = AddOrderToQuery(query, input.OrderBy, input.Order);
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((input.Page - 1) * input.PerPage)
                             .Take(input.PerPage)
                             .ToListAsync(cancellationToken);

            return new SearchOutput<Client>(
                input.Page,
                input.PerPage,
                totalCount,
                items
            );
        }

        private IQueryable<Client> AddOrderToQuery(IQueryable<Client> query, string orderBy, ESearchOrder order)
        {
            return (orderBy.ToLower(), order) switch
            {
                ("name", ESearchOrder.Asc) => query.OrderBy(c => c.Name),
                ("name", ESearchOrder.Desc) => query.OrderByDescending(c => c.Name),
                ("email", ESearchOrder.Asc) => query.OrderBy(c => c.Email),
                ("email", ESearchOrder.Desc) => query.OrderByDescending(c => c.Email),
                _ => query.OrderByDescending(c => c.CreatedAt)
            };
        }
    }
}
