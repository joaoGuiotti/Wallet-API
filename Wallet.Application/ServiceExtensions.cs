using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Wallet.Application.Events;
using Wallet.Application.Events.Transaction;
using Wallet.Application.UseCases.Account.CreateAccount;
using Wallet.Application.UseCases.Account.CreditAccount;
using Wallet.Application.UseCases.Account.DebitAccount;
using Wallet.Application.UseCases.Account.GetByIdAccount;
using Wallet.Application.UseCases.Client.CreateClient;
using Wallet.Application.UseCases.Client.GetByIdClient;
using Wallet.Application.UseCases.Transaction.CreateTransaction;
using Wallet.Application.UseCases.Transaction.GetByIdTransaction;
using Wallet.Domain.Events.Transaction;
using Wallet.Domain.Interfaces;


namespace Wallet.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        // Event Dispatcher
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        // MediatR Configuration - Registrar toda a assembly
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceExtensions).Assembly);
        });

        // services.AddScoped<IDomainEventHandler<TransactionCreatedEvent>, TransactionCreatedEventHandler>();

        // Domain Events
        services.Scan(scan => scan
            .FromAssemblyOf<IDomainEventHandler<IDomainEvent>>()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}

