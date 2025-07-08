using Wallet.Domain.Entities;
using Wallet.Domain.Repository;

namespace Wallet.Application.Interfaces.Repositories
{
    public interface IAccountRepository
        : IRepository<Account>, ISearchableRepository<Account>
    { }
}
