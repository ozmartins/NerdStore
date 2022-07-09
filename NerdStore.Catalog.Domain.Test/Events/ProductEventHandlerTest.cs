using FluentAssertions;
using NerdStore.Catalog.Domain.Events;
using System;
using System.Threading;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Events
{
    public class ProductEventHandlerTest
    {
        [Fact]
        public void WhenCallHandleThrownException()
        {
            new ProductEventHandler()
                .Invoking(x => x.Handle(new ProductOutOfStockEvent(Guid.Empty), CancellationToken.None))
                .Should()
                .ThrowAsync<NotImplementedException>();
        }
    }
}
