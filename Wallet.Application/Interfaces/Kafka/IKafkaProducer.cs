using System;

namespace Wallet.Application.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, string message, CancellationToken cancellationToken);
}
