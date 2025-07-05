namespace Wallet.Tests.Units.Domain.Client;

using Wallet.Domain.Entities;
using Wallet.Domain.Shared;


public class ClientTests
{
    [Fact]
    public void Constructor_ShouldInitializeClientCorrectly()
    {
        var client = new Client("Jhonny", "jhony@gmail.com");
        Assert.Equal("Jhonny", client.Name);
        Assert.Equal("jhony@gmail.com", client.Email);
        Assert.Empty(client.Accounts);
    }

    [Fact]
    public void Constructor_ShouldThrowErrorsWhenInitialize()
    {
        // Testa nome vazio
        var ex1 = Assert.Throws<NotificationException>(() =>
        {
            new Client("", "email@example.com");
        });
        Assert.Equal("Client: Name is required", ex1.Message);

        // Testa email vazio
        var ex2 = Assert.Throws<NotificationException>(() =>
        {
            new Client("Name", "");
        });
        Assert.Equal("Client: Email is required, Client: Email is invalid", ex2.Message);

        // testar mais de uma exception
        var ex3 = Assert.Throws<NotificationException>(() =>
        {
            new Client("", "");
        });
        Assert.Equal("Client: Name is required, Client: Email is required, Client: Email is invalid", ex3.Message);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName()
    {
        var client = new Client("Old Name", "email@example.com");
        client.ChangeName("New Name");
        Assert.Equal("New Name", client.Name);
    }

    [Fact]
    public void ChangeName_ShouldThrowError()
    {
        var ex = Assert.Throws<NotificationException>(() =>
        {
            new Client("", "email@example.com");
        });
        Assert.Equal("Client: Name is required", ex.Message);
    }

    [Fact]
    public void ChangeEmail_ShouldUpdateEmail()
    {
        var client = new Client("Name", "old@email.com");
        client.ChangeEmail("new@email.com");
        Assert.Equal("new@email.com", client.Email);
    }

    [Fact]
    public void ChangeEmail_ShouldThrowError()
    {
        var ex = Assert.Throws<NotificationException>(() =>
        {
            new Client("Name", "");
        });
        Assert.Equal("Client: Email is required, Client: Email is invalid", ex.Message);
    }

    [Fact]
    public void AddAccount_ShouldReplaceAccountsList()
    {
        var client = new Client("Name", "email@example.com");
        var accounts = new Account[]
        {
            new Account(client, 100),
            new Account(client, 200),
        };
        client.AddAccount(accounts[0]);
        client.AddAccount(accounts[1]);
        Assert.Equal(2, client.Accounts.Count);
    }

    [Fact]
    public void AddAccount_ShouldThrowError()
    {
        var client = new Client("Name", "email@example.com");
        var client2 = new Client("Name 2", "email@exaple.com");
        var accounts = new Account[]
        {
            new Account(client, 100),
            new Account(client2, 200),
        };
        client.AddAccount(accounts[0]);
        var error = Assert.Throws<NotificationException>(() =>
        {
            client.AddAccount(accounts[1]);
        });
        Assert.Equal("Client: Account does not belong to client", error.Message);
    }
}
