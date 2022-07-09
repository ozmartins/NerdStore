using FluentAssertions;
using NerdStore.Catalog.Domain.Events;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Events
{
    public class ProductOutOfStockEventTest
    {
        [Fact]
        public void CreateProductOutOfStockEventTest()
        {
            var guid = Guid.NewGuid();
            var productOutOfStockEvent = new ProductOutOfStockEvent(guid);
            productOutOfStockEvent.AggregateId.Should().Be(guid);
        }
    }
}
