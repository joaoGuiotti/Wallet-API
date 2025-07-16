using Wallet.Domain.Constants;
using Wallet.Domain.Repository;

namespace Wallet.Application.Common;

public abstract class PaginatedListInput
{
    public PaginatedListInput(
        int page = PaginateListConstants.DefaultPage,
        int perPage = PaginateListConstants.DefaultPerPage,
        string searchTerm = PaginateListConstants.DefaultSearchTerm,
        string sort = PaginateListConstants.DefaultSort,
        ESearchOrder dir = PaginateListConstants.DefaultDir
    )
    {
        Page = page;
        PerPage = perPage;
        SearchTerm = searchTerm;
        Sort = sort;
        Dir = dir;
    }

    public int Page { get; set; }
    public int PerPage { get; set; }
    public string SearchTerm { get; set; }
    public string Sort { get; set; }
    public ESearchOrder Dir { get; set; }

    public SearchInput ToSearchInput()
        => new(Page, PerPage, SearchTerm, Sort, Dir);
}
