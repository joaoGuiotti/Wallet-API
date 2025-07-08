using MediatR;
using Wallet.Domain.Interfaces;
using Wallet.Domain.Shared;

namespace Wallet.Application.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<IDomainEventHandler<IDomainEvent>>> _eventHandlers = new();

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> initialEvents, CancellationToken cancellationToken = default)
        {
            var eventQueue = new Queue<IDomainEvent>(initialEvents);

            while (eventQueue.Count > 0)
            {
                var currentEvent = eventQueue.Dequeue();

                // Publish the current event
                await _mediator.Publish(currentEvent, cancellationToken);

                // If the current event is associated with an aggregate, check for new events
                if (currentEvent is AggregateRoot aggregateRoot)
                {
                    var additionalEvents = aggregateRoot.PopEvents();
                    foreach (var additionalEvent in additionalEvents)
                    {
                        eventQueue.Enqueue(additionalEvent);
                    }
                }
            }
        }
    }
}
