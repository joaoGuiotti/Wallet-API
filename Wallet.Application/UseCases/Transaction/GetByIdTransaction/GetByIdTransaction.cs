using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Transaction.Common;

namespace Wallet.Application.UseCases.Transaction.GetByIdTransaction;

public class GetByIdTransaction : IGetByIdTransaction
{
    private readonly ITransactionRepository _transactionRepo;

    public GetByIdTransaction(ITransactionRepository transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public async Task<TransactionModelOutput> Handle(GetByIdTransactionInput request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepo.Find(request.TransactionId, cancellationToken);
        return TransactionModelOutput.FromTransaction(transaction!);
    }
}
