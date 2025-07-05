using MediatR;
using Wallet.Application.UseCases.Transaction.Common;

namespace Wallet.Application.UseCases.Transaction.GetByIdTransaction;

public interface IGetByIdTransaction : IRequestHandler<GetByIdTransactionInput, TransactionModelOutput>
{ }
