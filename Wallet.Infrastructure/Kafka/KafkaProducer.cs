

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Wallet.Application.Kafka;

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(
        IConfiguration configuration,
        ILogger<KafkaProducer> logger
    )
    {
        _producer = CreateProducerBuild(configuration);
        _logger = logger;
    }

    public async Task ProduceAsync(string topic, string message, CancellationToken cancellationToken)
    {
        try
        {
            var deliveryResult = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message }, cancellationToken);
            _logger.LogInformation($"Delivery message to {deliveryResult.Value}, Offset: {deliveryResult.Offset}");
        }
        catch (ProduceException<Null, string> e)
        {
            _logger.LogError($"Delivery failed: {e.Error.Reason}");
        }
    }

    private IProducer<Null, string> CreateProducerBuild(IConfiguration configuration)
    {
        var config = new ProducerConfig();
        configuration.GetSection("Kafka:ProducerSettings").Bind(config);
        config.Acks = Acks.All;
        config.AllowAutoCreateTopics = true;
        return new ProducerBuilder<Null, string>(config).Build();
    }
}

