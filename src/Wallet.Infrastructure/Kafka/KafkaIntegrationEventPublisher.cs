

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wallet.Domain.Common;

namespace Wallet.Application.Kafka;

public class KafkaIntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaIntegrationEventPublisher> _logger;
    private readonly string? _defaultTopic;

    public KafkaIntegrationEventPublisher(
        IConfiguration configuration,
        ILogger<KafkaIntegrationEventPublisher> logger
    )
    {
        _producer = CreateProducerBuild(configuration);
        _logger = logger;
        _defaultTopic = configuration.GetSection("Kafka:Topics:Default").Value;
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        var topic = _defaultTopic ?? @event.GetType().Name;
        var message = JsonSerializer.Serialize(@event);
        try
        {
            var kafkaMessage = new Message<Null, string> { Value = message };
            var deliveryResult = await _producer.ProduceAsync(topic, kafkaMessage);
            _logger.LogInformation($"Delivered event to Kafka: {deliveryResult.TopicPartitionOffset}");
        }
        catch (ProduceException<Null, string> ex) 
        {
            _logger.LogError($"Failed to deliver event: {ex.Message}");
            throw;
        }
    }

    private IProducer<Null, string> CreateProducerBuild(IConfiguration configuration)
    {
        var config = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(config);
        config.Acks = Acks.All;
        config.EnableIdempotence = true;
        config.CompressionType = CompressionType.Snappy; // Optimize message size
        config.LingerMs = 5; // Batch messages for better throughput
        return new ProducerBuilder<Null, string>(config).Build();
    }
}

