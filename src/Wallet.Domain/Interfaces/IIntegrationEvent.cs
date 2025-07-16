namespace Wallet.Domain.Interfaces;

public interface IIntegrationEvent
{
    DateTime OccuredAt { get; }

    // get payload
    Object GetPayload();
}
