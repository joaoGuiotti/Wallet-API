using System;
using System.Text.Json;
using Wallet.Application.Interfaces.Events;
using Wallet.Application.Kafka;
using Wallet.Domain.Interfaces;

namespace Wallet.Infrastructure.Events;

public class KafkaEventDispatcher : IDomainDispatcher
{
    private IKafkaProducer _producer;

    public KafkaEventDispatcher(IKafkaProducer producer)
    {
        _producer = producer;
    }

    public async Task DispatchAsync(IEnumerable<IEvent> events, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            var topic = @event.GetType().Name;
            var message = JsonSerializer.Serialize(@event.GetPayload());

            await _producer.ProduceAsync(topic, message, cancellationToken);
        }
    }
}
