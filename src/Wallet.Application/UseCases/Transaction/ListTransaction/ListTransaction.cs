

using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;

namespace Wallet.Application.UseCases.Transaction.ListTransaction
{
    public class ListTransaction : UseCaseBase<ListTransactionInput, ListTransactionOutput>
    {
        private readonly ITransactionRepository _repo;

        public ListTransaction(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public override async Task<ListTransactionOutput> Handle(
            ListTransactionInput request,
            CancellationToken cancellationToken
        )
        {
            var result = await _repo.SearchAsync(request.ToSearchInput(), cancellationToken);
            return ListTransactionOutput.FromSearchOutput(result);
        }
    }
}
