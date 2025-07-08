using MediatR;
using Wallet.Application.Common;
using Wallet.Domain.Repository;

namespace Wallet.Application.UseCases.Account.ListAccount
{
    public sealed class ListAccountInput
        : PaginatedListInput, IRequest<ListAccountOutput>
    {
        public ListAccountInput(int page, int perPage, string searchTerm, string sort, ESearchOrder dir)
            : base(page, perPage, searchTerm, sort, dir)
        { }

        public ListAccountInput() : base()
        { }

    }
}