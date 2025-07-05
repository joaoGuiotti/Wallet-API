using DomainEntity = Wallet.Domain.Entities;

namespace Wallet.Application.UseCases.Client.Common;

public class ClientModelOutput
{
    public ClientModelOutput(
        Guid id,
        string name,
        string email,
        IReadOnlyList<ClientsModelOutputAccounts> accounts
    )
    {
        Id = id;
        Name = name;
        Email = email;
        Accounts = accounts;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IReadOnlyList<ClientsModelOutputAccounts> Accounts { get; set; }

    public static ClientModelOutput FromClient(DomainEntity.Client e)
        => new(
                e.Id,
                e.Name,
                e.Email,
                e.Accounts
                    .Select(ac => new ClientsModelOutputAccounts(ac.Id, ac.Balance))
                    .ToList().AsReadOnly()
            );

}

public class ClientsModelOutputAccounts
{
    public ClientsModelOutputAccounts(
        Guid id,
        float balance
    )
    {
        Id = id;
        Balance = balance;
    }

    public Guid Id { get; set; }
    public float Balance { get; set; }
}