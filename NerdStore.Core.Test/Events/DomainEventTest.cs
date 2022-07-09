using FluentAssertions;
using NerdStore.Core.Events;
using System;
using Xunit;

namespace NerdStore.Core.Test.Events
{
    public class DomainEventTest
    {
        [Fact]
        public void CreateDomainEvent()
        {
            var guid = Guid.NewGuid();
            
            var domainEvent = new DomainEvent(guid);

            domainEvent.AggregateId.Should().Be(guid);
            domainEvent.Type.Should().Be("DomainEvent");
            domainEvent.TimeStamp.Should().BeBefore(DateTime.Now);
        }
    }
}
