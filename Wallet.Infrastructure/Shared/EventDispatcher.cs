using Wallet.Domain.Interfaces;

namespace Wallet.Infrastructure.Common
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<string, List<IEventHandler<IEvent>>> _eventHandlers = new();

        public IReadOnlyList<IEventHandler<IEvent>>? GetEventHandler(string eventName)
        {
            return _eventHandlers.TryGetValue(eventName, out var handler)
                ? handler.AsReadOnly() : null;
        }

        public void Notify(IEvent @event)
        {
            var eventName = @event.GetType().Name;
            if (_eventHandlers.ContainsKey(eventName))
            {
                GetEventHandler(eventName)?.ToList().ForEach((handler) =>
                {
                    handler.Handle(@event);
                });
            }
        }

        public void Register(string eventName, IEventHandler<IEvent> eventHandler)
        {
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers[eventName] = new List<IEventHandler<IEvent>>();
            }

            _eventHandlers[eventName].Add(eventHandler);
        }

        public void Unregister(string eventName)
        {
            _eventHandlers.Remove(eventName);
        }

        public void UnregisterAll()
        {
            _eventHandlers.Clear();
        }
    }
}
