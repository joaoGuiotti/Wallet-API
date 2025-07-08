using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Account.Common;

public class AccountModelOutput
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public float Balance { get; set; }
    public float Limit { get; set; }

    public AccountModelOutput(
        Guid id,
        Guid clientId,
        float balance,
        float limit
    )
    {
        Id = id;
        ClientId = clientId;
        Balance = balance;
        Limit = limit;
    }

    public static AccountModelOutput FromAccount(
        DomainEntity.Account account
    ) => new(
            account.Id,
            account.ClientId,
            account.Balance,
            account.Limit
        );
}

