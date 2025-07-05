using System;
using Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.Interfaces.Repositories;

public interface ITransactionRepository : IRepository<Transaction>
{

}
