using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.UseCases.Account.CreateAccount;
using Wallet.Application.UseCases.Account.CreditAccount;
using Wallet.Application.UseCases.Account.DebitAccount;
using Wallet.Application.UseCases.Account.GetByIdAccount;
using Wallet.Application.UseCases.Client.CreateClient;
using Wallet.Application.UseCases.Client.GetByIdClient;
using Wallet.Application.UseCases.Transaction.CreateTransaction;
using Wallet.Application.UseCases.Transaction.GetByIdTransaction;

namespace Wallet.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        // Mediators

        // Clients
        services.AddMediatR(typeof(CreateClient));
        services.AddMediatR(typeof(GetByIdClient));
        
        // Accounts
        services.AddMediatR(typeof(GetByIdAccount));
        services.AddMediatR(typeof(CreateAccount));
        services.AddMediatR(typeof(CreditAccount));
        services.AddMediatR(typeof(DebitAccount));

        // Transactions
        services.AddMediatR(typeof(CreateTransaction));
        services.AddMediatR(typeof(GetByIdTransaction));

        return services;
    }
}
