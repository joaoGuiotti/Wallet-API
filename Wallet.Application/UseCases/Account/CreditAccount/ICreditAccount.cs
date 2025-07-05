using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.CreditAccount;

public interface ICreditAccount : IRequestHandler<CreditAccountInput, AccountModelOutput>
{

}
