using Wallet.Application.Common;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Domain.Repository;
using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Account.ListAccount;

public class ListAccountOutput
    : PaginatedListOutput<AccountModelOutput>
{
    public ListAccountOutput(int page, int perPage, int total, IReadOnlyList<AccountModelOutput> items)
        : base(page, perPage, total, items)
    { }

    public static ListAccountOutput FromSearchOutput(
        SearchOutput<DomainEntity.Account> searchOutput
    ) => new(
        searchOutput.CurrentPage,
        searchOutput.PerPage,
        searchOutput.Total,
        searchOutput.Items
            .Select(a => AccountModelOutput.FromAccount(a))
            .ToList().AsReadOnly()
    );
}
