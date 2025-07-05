using MediatR;

namespace Wallet.Application.UseCases.Account.GetAllAccount
{
    public class  ListAccountInput: IRequest
    {
        public string Search { get; set; }

        public ListAccountInput(string search = "")
        {
            Search = search;
        }
    }
}