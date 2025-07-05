using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Interfaces.Repositories;

public interface IClientRepository : IRepository<Client>
{
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
