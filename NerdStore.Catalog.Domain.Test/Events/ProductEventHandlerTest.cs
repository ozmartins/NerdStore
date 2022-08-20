using FluentAssertions;
using NerdStore.Catalog.Domain.Events;
using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Events
{
    public class ProductEventHandlerTest
    {
        [Fact]
        public async void WhenCallHandleThrownException()
        {
            //arrange
            var logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger("WhenCallHandleThrownException");

            var product = new Product(
                Guid.NewGuid(), 
                "Name 1",
                "Description 1", 
                "Image 1", 
                1, 
                2,
                new Dimensions(3, 4, 5), 
                new Category(Guid.NewGuid(), 6, "Category 1"));

            var repositoryMock = new Mock<IProductRepository>(MockBehavior.Strict);
            repositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            var notification = new ProductOutOfStockEvent(product.Id);

            var productEventHandler = new ProductEventHandler(logger, repositoryMock.Object);

            //act
            await productEventHandler.Handle(notification, CancellationToken.None);

            //assert
            repositoryMock.VerifyAll();
        }
    }
}