using System;
using Wallet.Application.Common;

namespace Wallet.API.Models
{
    public class ApiResponseList<TItemData>
        : ApiResponse<IReadOnlyList<TItemData>>
    {
        public ApiResponseListMeta Meta { get; private set; }

        public ApiResponseList(
            int currentPage,
            int perPage,
            int total,
            IReadOnlyList<TItemData> data
        ) : base(data)
        {
            Meta = new ApiResponseListMeta(currentPage, perPage, total);
        }

        public ApiResponseList(
            PaginatedListOutput<TItemData> paginatedListOutput
        ) : base(paginatedListOutput.Items)
        {
            Meta = new ApiResponseListMeta(
                paginatedListOutput.Page,
                paginatedListOutput.PerPage,
                paginatedListOutput.Total
            );
        }
    }

    public class ApiResponseListMeta
    {
        public int CurrentPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }

        public ApiResponseListMeta(int currentPage, int perPage, int total)
        {
            CurrentPage = currentPage;
            PerPage = perPage;
            Total = total;
        }
    }
}
