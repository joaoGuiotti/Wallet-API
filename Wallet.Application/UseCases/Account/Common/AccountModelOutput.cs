using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Account.Common;

public class AccountModelOutput
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public float Balance { get; set; }

    public AccountModelOutput(
        Guid id,
        Guid clientId,
        float balance
    )
    {
        Id = id;
        ClientId = clientId;
        Balance = balance;
    }

    public static AccountModelOutput FromAccount(
        DomainEntity.Account account
    ) => new(
            account.Id,
            account.ClientId,
            account.Balance
        );
}

