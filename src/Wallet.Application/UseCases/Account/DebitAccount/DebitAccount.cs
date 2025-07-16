using Wallet.Application.Abstractions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Domain.Interfaces;

namespace Wallet.Application.UseCases.Account.DebitAccount;

public class DebitAccount : UseCaseBase<DebitAccountInput, AccountModelOutput>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DebitAccount(
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork
    )
    {
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<AccountModelOutput> Handle(DebitAccountInput request, CancellationToken cancelationToken)
    {
        var account = await _accountRepository.Find(request.accountId, cancelationToken);
        if (account is null)
        {
            throw new InvalidOperationException($"Account with Id {request.accountId} was not found.");
        }
        if (request.amount <= 0)
        {
            throw new InvalidOperationException($"Ammount must be greather than zero.");
        }

        account.Debit(request.amount);
        await _unitOfWork.DoAsync(async uow =>
        {
            await _accountRepository.Update(account, cancelationToken);
        }, cancelationToken);

        return AccountModelOutput.FromAccount(account);
    }
}
