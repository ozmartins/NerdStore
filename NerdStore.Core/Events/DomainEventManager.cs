using MediatR;

namespace NerdStore.Core.Events
{
    public class DomainEventManager
    {
        private readonly IMediator _mediator;

        public DomainEventManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void PublishEvent(DomainEvent domainEvent)
        { 
            _mediator.Publish(domainEvent);
        }
    }
}
