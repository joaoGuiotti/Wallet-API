
namespace Wallet.Domain.Interfaces
{
    public interface IEventDispatcher
    {
        public IReadOnlyList<IEventHandler<IEvent>>? GetEventHandler(string eventName);
        public void Notify(IEvent @event);
        public void Register(string eventName, IEventHandler<IEvent> IEvehandler);
        public void Unregister(string eventName);
        public void UnregisterAll();
    }
}
