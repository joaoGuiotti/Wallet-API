namespace Wallet.Application.Common;

public abstract class PaginatedListOutput<TOutputItem>
{
    protected PaginatedListOutput(
        int page,
        int perPage,
        int total,
        IReadOnlyList<TOutputItem> items)
    {
        Page = page;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutputItem> Items { get; set; }
}
