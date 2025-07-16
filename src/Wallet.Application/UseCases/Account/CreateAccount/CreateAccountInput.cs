using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.CreateAccount
{
    public record CreateAccountInput(Guid clientId, float balance) : IRequest<AccountModelOutput>;
}