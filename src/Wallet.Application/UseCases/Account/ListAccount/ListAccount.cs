using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Application.UseCases.Account.ListAccount;
using Wallet.Domain.Repository;

namespace Wallet.Application.UseCases.Account.GetAllAccount
{
    public class ListAccountUseCase : UseCaseBase<ListAccountInput, ListAccountOutput>
    {
        public readonly IAccountRepository _accountRepository;

        public ListAccountUseCase(
            IAccountRepository accountrepository
        )
        {
            _accountRepository = accountrepository;
        }

        public override async Task<ListAccountOutput> Handle(ListAccountInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _accountRepository.SearchAsync(request.ToSearchInput(), cancellationToken);
            return ListAccountOutput.FromSearchOutput(searchOutput);
        }
    }
}
