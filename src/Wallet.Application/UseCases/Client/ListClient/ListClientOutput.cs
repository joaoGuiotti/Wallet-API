using Wallet.Application.Common;
using Wallet.Application.UseCases.Client.Common;
using Wallet.Domain.Repository;
using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Client.ListClient
{
    public sealed class ListClientOutput : PaginatedListOutput<ClientModelOutput>
    {
        public ListClientOutput(int page, int perPage, int total, IReadOnlyList<ClientModelOutput> items)
            : base(page, perPage, total, items) { }

        public static ListClientOutput FromSearchOutput(
            SearchOutput<DomainEntity.Client> search
        ) =>
            new(
                search.CurrentPage,
                search.PerPage,
                search.Total,
                search.Items
                    .Select(ClientModelOutput.FromClient)
                    .ToList().AsReadOnly()
            );
    }
}
