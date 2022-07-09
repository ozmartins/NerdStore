using FluentAssertions;
using NerdStore.Catalog.Domain.Entities;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Entities
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
        public void CreateValidProduct()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            product.Name.Should().Be("Name");
            product.Description.Should().Be("Description");
            product.Image.Should().Be("Image");
            product.Price.Should().Be(1);
            product.Quantity.Should().Be(2);
            product.Dimensions.Height.Should().Be(3);
            product.Dimensions.Width.Should().Be(4);
            product.Dimensions.Depth.Should().Be(5);
            product.Category.Code.Should().Be(6);
            product.Category.Name.Should().Be("Test");
        }        

        [Fact]
        public void DeactivateActivateProduct()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            product.Deactivate();
            product.Active.Should().Be(false);

            product.Activate();
            product.Active.Should().Be(true);
        }

        [Fact]
        public void ChangeDescriptionUsingInvalidDescription()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            var exception = Assert.Throws<Exception>(() => product.ChangeDescription(string.Empty));

            exception.Message.Should().Be("Description can't be empty.");
        }

        [Fact]
        public void ChangeDescriptionUsingValidDescription()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            product.ChangeDescription("New description");

            product.Description.Should().Be("New description");
        }

        [Fact]
        public void ChangeCategoryUsingNullCategory()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            var exception = Assert.Throws<Exception>(() => product.ChangeCategory(null!));

            exception.Message.Should().Be("Category can't be empty.");
        }

        [Fact]
        public void ChangeCategoryUsingValidCategory()
        {
            var product = new Product("Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(6, "Test"));

            product.ChangeCategory(new Category(7, "New category"));

            product.Category.Code.Should().Be(7);
            product.Category.Name.Should().Be("New category");
        }
    }
}
