using MediatR;
using Wallet.Application.UseCases.Transaction.Common;

namespace Wallet.Application.UseCases.Transaction.GetByIdTransaction;

public sealed record GetByIdTransactionInput(Guid TransactionId) : IRequest<TransactionModelOutput>
{ }
