using FluentAssertions;
using Moq;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Events;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test
{
    public class InventoryServiceTest
    {
        [Fact]
        public void ReplenishStockWithEmptyGuid()
        {
            new InventoryService(null!, null!)
                .Invoking(x => x.ReplenishStock(Guid.Empty, 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Product ID can't be empty.");
        }

        [Fact]
        public void ReplenishStockWithQuantityLessThanOne()
        {            
            new InventoryService(null!, null!)
                .Invoking(x => x.ReplenishStock(Guid.NewGuid(), 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Quantity must to be greater than zero.");                
        }

        [Fact]
        public void ReplenishStockWithNonExistentProduct()
        {            
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<Product>(null);

            new InventoryService(productRepositoryMock.Object, null!)
                .Invoking(x => x.ReplenishStock(Guid.NewGuid(), 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Product not found.");                
        }

        [Fact]
        public async void ReplenishStockWithValidProduct()
        {
            var product = new Product(Guid.NewGuid(), "Product name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Category name"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
            productRepositoryMock.Setup(x => x.Commit());

            var inventoryService = new InventoryService(productRepositoryMock.Object, null!);
            await inventoryService.ReplenishStock(product.Id, 10);
            
            product.Quantity.Should().Be(12);
            productRepositoryMock.Verify(x => x.Update(product));
            productRepositoryMock.Verify(x => x.Commit());
        }      

        [Fact]
        public void WithdrawFromStockWithEmptyGuid()
        {
            new InventoryService(null!, null!)
                .Invoking(x => x.WithdrawFromStock(Guid.Empty, 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Product ID can't be empty.");
        }

        [Fact]
        public void WithdrawFromStockWithQuantityLessThanOne()
        {
            new InventoryService(null!, null!)
                .Invoking(x => x.WithdrawFromStock(Guid.NewGuid(), 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Quantity must to be greater than zero.");
        }

        [Fact]
        public void WithdrawFromStockWithNonExistentProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns<Product>(null);

            new InventoryService(productRepositoryMock.Object, null!)
                .Invoking(x => x.WithdrawFromStock(Guid.NewGuid(), 0))
                .Should()
                .ThrowAsync<Exception>()
                .WithMessage("Product not found.");
        }

        [Fact]
        public async void WithdrawFromStockWithValidProduct()
        {
            var product = new Product(Guid.NewGuid(), "Product name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Category name"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
            productRepositoryMock.Setup(x => x.Commit()); ;

            var inventoryService = new InventoryService(productRepositoryMock.Object, null!);
            await inventoryService.WithdrawFromStock(product.Id, 1);
            
            product.Quantity.Should().Be(1);
            productRepositoryMock.Verify(x => x.Update(product));
            productRepositoryMock.Verify(x => x.Commit());            
        }

        [Fact]
        public async void WithdrawFromStockWithValidProductAndConsumingTheWholeStock()
        {
            var product = new Product(Guid.NewGuid(), "Product name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Category name"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
            productRepositoryMock.Setup(x => x.Commit());

            var domainEventManagerMock = new Mock<IDomainEventManager>();
            domainEventManagerMock.Setup(x => x.PublishEvent(It.IsAny<DomainEvent>()));

            var inventoryService = new InventoryService(productRepositoryMock.Object, domainEventManagerMock.Object);
            await inventoryService.WithdrawFromStock(product.Id, 2);
           
            product.Quantity.Should().Be(0);
            productRepositoryMock.Verify(x => x.Update(product));
            productRepositoryMock.Verify(x => x.Commit());
            domainEventManagerMock.Verify(x => x.PublishEvent(It.IsAny<DomainEvent>()));
        }

        [Fact]
        public void WithdrawFromStockWithValidProductAndConsumingMoreThanIsAvaliable()
        {
            var product = new Product(Guid.NewGuid(), "Product name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Category name"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);
            productRepositoryMock.Setup(x => x.Commit()); 

            var inventoryService = new InventoryService(productRepositoryMock.Object, null!);
            inventoryService.Invoking(x => x.WithdrawFromStock(product.Id, 3)).Should().ThrowAsync<Exception>().WithMessage("There are not enough items in inventory.");
        }
    }
}
