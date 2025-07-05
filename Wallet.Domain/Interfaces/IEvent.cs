namespace Wallet.Domain.Interfaces
{
    public interface IEvent
    {
        DateTime OccuredAt { get; }

        // get payload
        public abstract Object GetPayload();
    }
}
