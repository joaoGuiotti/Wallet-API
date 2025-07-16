using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.CreditAccount;

public record CreditAccountInput(Guid accountId, float amount): IRequest<AccountModelOutput>;
