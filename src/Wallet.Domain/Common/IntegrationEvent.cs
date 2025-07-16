using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Common;

public class IntegrationEvent : IIntegrationEvent
{
    public DateTime OccuredAt => DateTime.Now;

    public object GetPayload()
        => this;

    protected IntegrationEvent() { }
}
