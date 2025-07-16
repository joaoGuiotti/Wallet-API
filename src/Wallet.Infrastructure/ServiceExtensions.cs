using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wallet.Application.Interfaces.Events;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.Kafka;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Kafka;
using Wallet.Infrastructure.Persistence.Context;
using Wallet.Infrastructure.Repositories;

namespace Wallet.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<WalletDbContext>(options => options.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 36))
            ));

        // Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // Register KafkaIntegrationEventPublisher
        services.AddSingleton<IIntegrationEventPublisher>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<KafkaIntegrationEventPublisher>>();
            return new KafkaIntegrationEventPublisher(configuration, logger);
        });

        services.AddScoped<IIntegrationEventPublisher, KafkaIntegrationEventPublisher>();
        services.AddScoped<IKafkaConsumer, KafkaConsumer>();

        // Background Services
        services.AddHostedService<KafkaConsumerBackgroundService>();

        return services;
    }

}
