using Wallet.Application.Common;

namespace Wallet.Application.UseCases.Transaction.ListTransaction
{
    public sealed class ListTransactionInput :
        PaginatedListInput, MediatR.IRequest<ListTransactionOutput>
    { }

}
