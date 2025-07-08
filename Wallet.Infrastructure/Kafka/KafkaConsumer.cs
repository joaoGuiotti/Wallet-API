using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wallet.Application.Events.Transaction;
using Wallet.Application.Kafka;

namespace Wallet.Infrastructure.Kafka;

public class KafkaConsumer : IKafkaConsumer, IDisposable
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly string _topicName;
    private CancellationTokenSource _cancellationTokenSource;

    public KafkaConsumer(
        IConfiguration configuration,
        ILogger<KafkaConsumer> logger
    )
    {
        _consumer = CreateConsumerBuild(configuration);
        _logger = logger;
        _topicName = configuration.GetValue<string>("Kafka:Topics:Transactions") ?? "";
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting Kafka consumer for topic: {_topicName}");
        
        _consumer.Subscribe(_topicName);

        await Task.Run(async () =>
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);
                        
                        if (consumeResult?.Message?.Value != null)
                        {
                            await ProcessMessage(consumeResult.Message.Value);
                            _consumer.Commit(consumeResult);
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Error consuming message from Kafka");
                    }
                    catch (OperationCanceledException)
                    {
                        _logger.LogInformation("Kafka consumer operation was cancelled");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fatal error in Kafka consumer");
            }
            finally
            {
                _consumer.Close();
            }
        }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Kafka consumer");
        _cancellationTokenSource.Cancel();
        return Task.CompletedTask;
    }

    private async Task ProcessMessage(string message)
    {
        try
        {
            _logger.LogInformation($"Received message: {message}");

            var transactionEvent = JsonSerializer.Deserialize<TransactionCreatedIntegrationEvent>(message);
            
            if (transactionEvent != null)
            {
                _logger.LogInformation($"Processing TransactionCreatedEvent for Transaction ID: {transactionEvent.TransactionId}");

                await ProcessTransactionCreatedEvent(transactionEvent);
            }
            else
            {
                _logger.LogWarning("Failed to deserialize message as TransactionCreatedEvent");
            }
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, $"Error deserializing message: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error processing message: {message}");
        }
    }

    private Task ProcessTransactionCreatedEvent(TransactionCreatedIntegrationEvent transactionEvent)
    {
        _logger.LogInformation($"🎉 Transaction {transactionEvent.TransactionId} was created successfully!");
        _logger.LogInformation($"Event timestamp: {DateTime.UtcNow}");
        
        _logger.LogInformation($"✅ Transaction {transactionEvent.TransactionId} event processed successfully!");
        return Task.CompletedTask;
    }

    private IConsumer<Ignore, string> CreateConsumerBuild(IConfiguration configuration)
    {
        var config = new ConsumerConfig();
        configuration.GetSection("Kafka:ConsumerSettings").Bind(config);
        
        config.GroupId = configuration.GetValue<string>("Kafka:ConsumerSettings:GroupId") ?? "wallet-consumer-group";
        config.AutoOffsetReset = AutoOffsetReset.Earliest;
        config.EnableAutoCommit = false; // Commit manual para garantir processamento
        
        return new ConsumerBuilder<Ignore, string>(config).Build();
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _consumer?.Dispose();
    }
}
