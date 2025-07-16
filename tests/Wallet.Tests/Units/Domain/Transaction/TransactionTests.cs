using Wallet.Domain.Shared;
using DomainEnity = Wallet.Domain.Entities;

namespace Wallet.Tests.Units.Domain.Transaction;

public class TransactionTests
{
    private DomainEnity.Account CreateAccount(float balance = 0)
    {
        // Crie um cliente fictício para o Account
        var client = new DomainEnity.Client("Test", "test@mail.com");
        return new DomainEnity.Account(client, balance);
    }

    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var accountFrom = CreateAccount(100);
        var accountTo = CreateAccount(50);
        float amount = 30;

        // Act
        var transaction = new DomainEnity.Transaction(accountFrom, accountTo, amount);

        // Assert
        Assert.Equal(accountFrom, transaction.AccountFrom);
        Assert.Equal(accountFrom.Id, transaction.AccountFromId);
        Assert.Equal(accountTo, transaction.AccountTo);
        Assert.Equal(accountTo.Id, transaction.AccountToId);
        Assert.Equal(amount, transaction.Amount);
    }

    [Fact]
    public void Commit_ShouldTransferAmount()
    {
        // Arrange
        var accountFrom = CreateAccount(100);
        var accountTo = CreateAccount(50);
        float amount = 40;
        var transaction = new DomainEnity.Transaction(accountFrom, accountTo, amount);
        transaction.Commit();

        // Assert
        Assert.Equal(60, accountFrom.Balance);
        Assert.Equal(90, accountTo.Balance);
    }

    [Fact]
    public void Validate_ShouldThrow_WhenAmountIsZero()
    {
        var accountFrom = CreateAccount(100);
        var accountTo = CreateAccount(50);

        Assert.Throws<NotificationException>(() =>
        {
            new DomainEnity.Transaction(accountFrom, accountTo, 0);
        });
    }

}
