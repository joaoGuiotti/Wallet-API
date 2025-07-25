using Wallet.Domain.Shared;

namespace Wallet.Domain.Repository;

public class SearchOutput<TAggregate>
    where TAggregate : AggregateRoot
{
    public SearchOutput(
     int currentPage,
     int perPage,
     int total,
     IReadOnlyList<TAggregate> items
    )
    {
        CurrentPage = currentPage;
        PerPage = perPage;
        Total = total;
        Items = items;
    }

    public int CurrentPage { get; }
    public int PerPage { get; }
    public int Total { get; }
    public IReadOnlyList<TAggregate> Items { get; }
}