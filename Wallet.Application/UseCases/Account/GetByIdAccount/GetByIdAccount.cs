using System;
using Wallet.Application.Abstractions;
using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;

namespace Wallet.Application.UseCases.Account.GetByIdAccount;

public class GetByIdAccount : UseCaseBase<GetByIdAccountInput, AccountModelOutput>
{
    private readonly IAccountRepository _accountRepo;

    public GetByIdAccount(
        IAccountRepository repo
    )
    {
        _accountRepo = repo;
    }
    public override async Task<AccountModelOutput> Handle(GetByIdAccountInput request, CancellationToken cancellationToken)
    {
        var account = await _accountRepo.Find(request.accountId, cancellationToken);
        NotFoundException.ThrowIfNull(account, $"{nameof(Account)} {request.accountId} is not Found");
        return AccountModelOutput.FromAccount(account!);
    }
}
