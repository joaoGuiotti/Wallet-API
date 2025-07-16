using MediatR;
using Wallet.Application.Common;

namespace Wallet.Application.UseCases.Client.ListClient
{
    public sealed class ListClientInput : PaginatedListInput, IRequest<ListClientOutput>
    {  }
}
