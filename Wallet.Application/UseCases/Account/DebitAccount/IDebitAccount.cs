using System;
using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.DebitAccount;

public interface IDebitAccount : IRequestHandler<DebitAccountInput, AccountModelOutput>
{

}
