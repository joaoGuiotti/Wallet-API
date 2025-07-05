using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Transaction.Common;

public class TransactionModelOutput
{
    public TransactionModelOutput(
        Guid id,
        TrasactionsModelOutputAccounts accountFrom,
        TrasactionsModelOutputAccounts accountTo,
        float amount
    )
    {
        Id = id;
        AccountFrom = accountFrom;
        AccountTo = accountTo;
        Amount = amount;
    }

    public Guid Id { get; private set; }
    public TrasactionsModelOutputAccounts AccountFrom { get; private set; }
    public TrasactionsModelOutputAccounts AccountTo { get; private set; }
    public float Amount { get; private set; }

    public static TransactionModelOutput FromTransaction(DomainEntity.Transaction e)
        => new(
            e.Id,
            new TrasactionsModelOutputAccounts(e.AccountFrom!.Id, e.AccountFrom!.Balance),
            new TrasactionsModelOutputAccounts(e.AccountTo!.Id, e.AccountTo!.Balance),
            e.Amount
        );
}

public sealed record TrasactionsModelOutputAccounts(Guid Id, float Balance); 
 