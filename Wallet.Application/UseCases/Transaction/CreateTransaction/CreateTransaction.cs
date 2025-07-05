using Wallet.Application.Exceptions;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.UseCases.Transaction.Common;
using DomainEntity = Wallet.Domain.Entities;
using Wallet.Domain.Interfaces;
using Wallet.Application.Interfaces.Events;

namespace Wallet.Application.UseCases.Transaction.CreateTransaction;

public class CreateTransaction : ICreateTransaction
{
    public CreateTransaction(
        ITransactionRepository transactionRepo,
        IAccountRepository accountRepo,
        IUnitOfWork unitOfWork,
        IDomainDispatcher dispatcher
    )
    {
        _transactionRepo = transactionRepo;
        _accountRepo = accountRepo;
        _unitOfWork = unitOfWork;
        _dispatcher = dispatcher;
    }

    private readonly ITransactionRepository _transactionRepo;
    private readonly IAccountRepository _accountRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainDispatcher _dispatcher;

    public async Task<TransactionModelOutput> Handle(CreateTransactionInput request, CancellationToken cancellationToken)
    {
        var accountFrom = await _accountRepo.Find(request.AccountFromId, cancellationToken);
        NotFoundException.ThrowIfNull(accountFrom, $"AccountFrom {request.AccountFromId} not found.");

        var accountTo = await _accountRepo.Find(request.AccountToId, cancellationToken);
        NotFoundException.ThrowIfNull(accountTo, $"AccountTo {request.AccountToId} not found.");

        var transaction = new DomainEntity.Transaction(accountFrom!, accountTo!, request.Amount);
        transaction.Commit(); // Aplica os efeitos da transação, debitando e creditando as contas

        await _unitOfWork.DoAsync(async uow =>
        {
            await _accountRepo.Update(transaction.AccountFrom, cancellationToken);
            await _accountRepo.Update(transaction.AccountTo, cancellationToken);

            await _transactionRepo.Create(transaction, cancellationToken);
        }, cancellationToken);

        await _dispatcher.DispatchAsync(transaction.GetUncommittedEvents(), cancellationToken);
        transaction.ClearEvents();

        return TransactionModelOutput.FromTransaction(transaction);
    }
}
