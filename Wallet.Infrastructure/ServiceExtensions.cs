using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wallet.Application.Interfaces.Events;
using Wallet.Application.Interfaces.Repositories;
using Wallet.Application.Kafka;
using Wallet.Domain.Interfaces;
using Wallet.Infrastructure.Events;
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

        // Kafka
        services.AddScoped<IKafkaProducer, KafkaProducer>();
        services.AddScoped<IKafkaConsumer, KafkaConsumer>();
        services.AddScoped<IDomainDispatcher, KafkaEventDispatcher>();
        
        // Background Services
        services.AddHostedService<KafkaConsumerBackgroundService>();
        
        return services;
    }

}
