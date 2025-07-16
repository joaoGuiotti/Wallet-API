using Wallet.Domain.Entities;
using Wallet.Domain.Repository;

namespace Wallet.Application.Interfaces.Repositories;

public interface IClientRepository 
    : IRepository<Client>,
    ISearchableRepository<Client>
{
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
