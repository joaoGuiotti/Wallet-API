using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Transaction.Common;

public class TransactionModelOutput
{
    public TransactionModelOutput(
        Guid id,
        Guid accountFromId,
        Guid accountToId,
        float amount
    )
    {
        Id = id;
        AccountFromId = accountFromId;
        AccountToId = accountToId;
        Amount = amount;
    }

    public Guid Id { get; private set; }
    public Guid AccountFromId { get; private set; }
    public Guid AccountToId { get; private set; }
    public float Amount { get; private set; }

    public static TransactionModelOutput FromTransaction(DomainEntity.Transaction e)
        => new(
            e.Id,
            e.AccountFrom!.Id,
            e.AccountTo!.Id,
            e.Amount
        );
}

