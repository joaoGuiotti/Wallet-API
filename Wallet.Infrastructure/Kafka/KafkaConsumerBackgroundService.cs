using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wallet.Application.Kafka;

namespace Wallet.Infrastructure.Kafka;

public class KafkaConsumerBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<KafkaConsumerBackgroundService> _logger;

    public KafkaConsumerBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<KafkaConsumerBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Kafka Consumer Background Service started");

        using var scope = _serviceProvider.CreateScope();
        var kafkaConsumer = scope.ServiceProvider.GetRequiredService<IKafkaConsumer>();

        try
        {
            await kafkaConsumer.StartAsync(stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Kafka Consumer Background Service was cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fatal error in Kafka Consumer Background Service");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Kafka Consumer Background Service is stopping");
        await base.StopAsync(cancellationToken);
    }
}
