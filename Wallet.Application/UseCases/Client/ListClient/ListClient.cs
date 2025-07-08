using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;

namespace Wallet.Application.UseCases.Client.ListClient
{
    public class ListClient : UseCaseBase<ListClientInput, ListClientOutput>
    {
        private readonly IClientRepository _repo;

        public ListClient(
            IClientRepository repo
        )
        {
            _repo = repo;
        }

        public override async Task<ListClientOutput> Handle(ListClientInput request, CancellationToken cancellationToken)
        {
            var result = await _repo.SearchAsync(request.ToSearchInput(), cancellationToken);
            return ListClientOutput.FromSearchOutput(result);
        }
    }
}
