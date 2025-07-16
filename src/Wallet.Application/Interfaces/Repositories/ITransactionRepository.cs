using Wallet.Domain.Entities;
using Wallet.Domain.Repository;

namespace Wallet.Application.Interfaces.Repositories;

public interface ITransactionRepository 
    : IRepository<Transaction>, ISearchableRepository<Transaction>
{

}
