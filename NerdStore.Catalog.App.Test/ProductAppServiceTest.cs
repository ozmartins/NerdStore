using FluentAssertions;
using Moq;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace NerdStore.Catalog.App.Test
{
    public class ProductAppServiceTest
    {
        [Fact]
        public async void GetAllTest()
        {            
            var products = new List<Product> 
            { 
                new Product("Name 1", "Description 1", "Image 1", 001, 002, new Dimensions(003,004,005), new Category(006, "Category 1")),
                new Product("Name 2", "Description 2", "Image 2", 010, 020, new Dimensions(030,040,050), new Category(060, "Category 2")),
                new Product("Name 3", "Description 3", "Image 3", 100, 200, new Dimensions(300,400,500), new Category(600, "Category 3")),
            };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(products);

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            var foundProducts = await productAppService.GetAll();

            foundProducts.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async void GetByIdTest()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Category"));            

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            var foundProducts = await productAppService.GetById(Guid.Empty);

            foundProducts.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async void GetByCategory()
        {
            var products = new List<Product>
            {
                new Product("Name 1", "Description 1", "Image 1", 001, 002, new Dimensions(003,004,005), new Category(006, "Category 1")),
                new Product("Name 2", "Description 2", "Image 2", 010, 020, new Dimensions(030,040,050), new Category(060, "Category 2")),
                new Product("Name 3", "Description 3", "Image 3", 100, 200, new Dimensions(300,400,500), new Category(600, "Category 3")),
            };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetByCategory(It.IsAny<Guid>())).ReturnsAsync(products);

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            var foundProducts = await productAppService.GetByCategory(Guid.Empty);

            foundProducts.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async void GetCategoriesTest()
        {
            var categories = new List<Category> 
            { 
                new Category(1, "Name 1"),
                new Category(2, "Name 2"),
                new Category(3, "Name 3")
            };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetCategories()).ReturnsAsync(categories);

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            var foundCategories = await productAppService.GetCategories();

            foundCategories.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void AddTest()
        {
            var product = new Product("Name", "Description", "Image", 1, 20, new Dimensions(3, 4, 5), new Category(6, "Category"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()));
            productRepositoryMock.Setup(x => x.Commit());            

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            productAppService.Add(product);

            productRepositoryMock.Verify(x => x.Add(product));
            productRepositoryMock.Verify(x => x.Commit());
        }

        [Fact]
        public void UpdateTest()
        {
            var product = new Product("Name", "Description", "Image", 1, 20, new Dimensions(3, 4, 5), new Category(6, "Category"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()));
            productRepositoryMock.Setup(x => x.Commit());

            var productAppService = new ProductAppService(productRepositoryMock.Object, null!);

            productAppService.Update(product);

            productRepositoryMock.Verify(x => x.Update(product));
            productRepositoryMock.Verify(x => x.Commit());
        }

        [Fact]
        public async void ReplenishStockTest()
        {
            var product = new Product("Name", "Description", "Image", 1, 20, new Dimensions(3, 4, 5), new Category(6, "Category"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            var inventoryServiceMock = new Mock<IInventoryService>();
            inventoryServiceMock.Setup(x => x.ReplenishStock(It.IsAny<Guid>(), It.IsAny<int>()));

            var productAppService = new ProductAppService(productRepositoryMock.Object, inventoryServiceMock.Object);
            
            var returnedProduct = await productAppService.ReplenishStock(product.Id, 5);

            inventoryServiceMock.Verify(x => x.ReplenishStock(product.Id, 5));
            returnedProduct.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async void WithdrawFromStockTest()
        {
            var product = new Product("Name", "Description", "Image", 1, 20, new Dimensions(3, 4, 5), new Category(6, "Category"));

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(product);

            var inventoryServiceMock = new Mock<IInventoryService>();
            inventoryServiceMock.Setup(x => x.WithdrawFromStock(It.IsAny<Guid>(), It.IsAny<int>()));

            var productAppService = new ProductAppService(productRepositoryMock.Object, inventoryServiceMock.Object);

            var returnedProduct = await productAppService.WithdrawFromStock(product.Id, 5);

            inventoryServiceMock.Verify(x => x.WithdrawFromStock(product.Id, 5));
            returnedProduct.Should().BeEquivalentTo(product);
        }
    }
}