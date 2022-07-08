using FluentAssertions;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test
{
    public class ProductTest
    {
        [Fact]
        public void CreateProductWithInvalidName()
        {            
            var exception = Assert.Throws<Exception>(() => new Product(string.Empty, "Test", "Test", 1, 1, new Dimensions(1,1,1), new Category(1,"Test")));

            exception.Message.Should().Be("Name can't be empty.");
        }
        
        [Fact]
        public void CreateProductWithInvalidDescription()
        {
            var exception = Assert.Throws<Exception>(() => new Product("Test", string.Empty, "Test", 1, 1, new Dimensions(1, 1, 1), new Category(1, "Test")));

            exception.Message.Should().Be("Description can't be empty.");
        }

        [Fact]
        public void CreateProductWithInvalidImage()
        {
            var exception = Assert.Throws<Exception>(() => new Product("Test", "Test", string.Empty, 1, 1, new Dimensions(1, 1, 1), new Category(1, "Test")));

            exception.Message.Should().Be("Image can't be empty.");
        }

        [Fact]
        public void CreateProductWithInvalidPrice()
        {
            var exception = Assert.Throws<Exception>(() => new Product("Test", "Test", "Test", 0, 1, new Dimensions(1, 1, 1), new Category(1, "Test")));

            exception.Message.Should().Be("Price must to be greater than zero.");
        }

        [Fact]
        public void CreateProductWithInvalidQuantity()
        {
            var exception = Assert.Throws<Exception>(() => new Product("Test", "Test", "Test", 1, -1, new Dimensions(1, 1, 1), new Category(1, "Test")));

            exception.Message.Should().Be("Quantity must to be greater than zero.");
        }

        [Fact]
        public void CreateProductWithInvalidCategory()
        {
            var exception = Assert.Throws<Exception>(() => new Product("Test", "Test", "Test", 1, 1, new Dimensions(1, 1, 1), null!));

            exception.Message.Should().Be("Category can't be empty.");
        }

        [Fact]
        public void CreateValidProducto()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            product.Name.Should().Be("Name");
            product.Description.Should().Be("Description");
            product.Image.Should().Be("Image");
            product.Price.Should().Be(1);
            product.QuantityInInventory.Should().Be(2);
            product.Dimensions.Height.Should().Be(3);
            product.Dimensions.Width.Should().Be(4);
            product.Dimensions.Depth.Should().Be(5);
            product.Category.Code.Should().Be(6);
            product.Category.Name.Should().Be("Test");
        }
    }
}
