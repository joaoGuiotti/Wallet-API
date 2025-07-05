using Wallet.Domain.Interfaces;

namespace Wallet.Domain.Shared;

public class AggregateRoot : Entity
{
    private readonly HashSet<IEvent> _events = new();
    private readonly HashSet<IEvent> _dispatchedEvents = new();
    private readonly Dictionary<string, List<Action<IEvent>>> _handlers = new();

    public IReadOnlyCollection<IEvent> Events
        => _events.ToList().AsReadOnly();

    protected AggregateRoot() : base() { }

    public void ApplyEvent(IEvent @event)
    {
        _events.Add(@event);

        var eventName = @event.GetType().Name;

        if (_handlers.TryGetValue(eventName, out var handlers))
            foreach (var handler in handlers)
                handler(@event);
    }

    public void RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent : IEvent
    {
        var eventName = typeof(TEvent).Name;

        if (!_handlers.ContainsKey(eventName))
            _handlers[eventName] = new List<Action<IEvent>>();
        _handlers[eventName].Add(e => handler((TEvent)e));
    }

    public void MarkEventAsDispatcher(IEvent @event)
        => _dispatchedEvents.Add(@event);

    public IReadOnlyCollection<IEvent> GetUncommittedEvents()
        => _events.Except(_dispatchedEvents).ToList().AsReadOnly();

    public void ClearEvents()
    {
        _events.Clear();
        _dispatchedEvents.Clear();
    }
}
