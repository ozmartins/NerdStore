using MediatR;
using Moq;
using NerdStore.Core.Events;
using System;
using System.Threading;
using Xunit;

namespace NerdStore.Core.Test.Events
{
    public class DomainEventManagerTest
    {
        [Fact]
        public void PublishEventTest()
        {
            var domainEvent = new DomainEvent(Guid.NewGuid());

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Publish(domainEvent, CancellationToken.None));

            var domainEventManager = new DomainEventManager(mediatorMock.Object);

            domainEventManager.PublishEvent(domainEvent);

            mediatorMock.Verify(x => x.Publish(domainEvent, CancellationToken.None));
        }
    }
}
