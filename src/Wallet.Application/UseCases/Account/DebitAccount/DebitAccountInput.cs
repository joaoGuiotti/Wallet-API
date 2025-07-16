using MediatR;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.DebitAccount;

public record DebitAccountInput(Guid accountId, float amount): IRequest<AccountModelOutput>;
 