using Wallet.Domain.Constants;

namespace Wallet.Domain.Repository;

public class SearchInput
{
    public int Page { get; set; } = PaginateListConstants.DefaultPage;
    public int PerPage { get; set; } = PaginateListConstants.DefaultPerPage;
    public string SearchTerm { get; set; } = PaginateListConstants.DefaultSearchTerm;
    public string OrderBy { get; set; } = PaginateListConstants.DefaultSort;
    public ESearchOrder Order { get; set; } = PaginateListConstants.DefaultDir;

    public SearchInput(
     int page,
     int perPage,
     string searchTerm,
     string orderBy,
     ESearchOrder order
    )
    {
        Page = page;
        PerPage = perPage;
        SearchTerm = searchTerm;
        OrderBy = orderBy;
        Order = order;
    }
}
