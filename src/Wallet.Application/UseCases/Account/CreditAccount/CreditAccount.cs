using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.UseCases.Account.CreditAccount;

public class CreditAccount : UseCaseBase<CreditAccountInput, AccountModelOutput>
{
    public CreditAccount(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public override async Task<AccountModelOutput> Handle(CreditAccountInput request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.Find(request.accountId, cancellationToken);

        if (account is null)
            throw new InvalidOperationException($"Account with Id {request.accountId} was not found.");
        if (request.amount <= 0)
            throw new InvalidOperationException($"Ammount must be greather than zero.");

        account.Credit(request.amount);

        await _unitOfWork.DoAsync(async uow =>
        {
            await _accountRepository.Update(account, cancellationToken);
        }, cancellationToken);


        return AccountModelOutput.FromAccount(account);
    }


}
