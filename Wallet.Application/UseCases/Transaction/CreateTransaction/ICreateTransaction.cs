using MediatR;
using Wallet.Application.UseCases.Transaction.Common;

namespace Wallet.Application.UseCases.Transaction.CreateTransaction;

public interface ICreateTransaction : IRequestHandler<CreateTransactionInput, TransactionModelOutput> {}
