using System;
using Wallet.Domain.Common;

namespace Wallet.Application.Kafka;

public interface IIntegrationEventPublisher
{
    Task PublishAsync<T>(T @event) where T : IntegrationEvent;
}
