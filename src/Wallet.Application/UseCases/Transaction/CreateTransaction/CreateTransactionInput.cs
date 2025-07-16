
using MediatR;
using Wallet.Application.UseCases.Transaction.Common;

namespace Wallet.Application.UseCases.Transaction.CreateTransaction;

public sealed record CreateTransactionInput(
    Guid AccountFromId,
    Guid AccountToId,
    float Amount
) : IRequest<TransactionModelOutput>;
