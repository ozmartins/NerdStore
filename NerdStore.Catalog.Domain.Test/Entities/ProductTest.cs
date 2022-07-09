using FluentAssertions;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Entities
{
    public class ProductTest
    {
        [Fact]
        public void CreateProductWithInvalidName()
        {            
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), string.Empty, "Test", "Test", 1, 1, new Dimensions(1,1,1), new Category(Guid.NewGuid(), 1,"Test")));

            exception.Message.Should().Be("Name can't be empty.");
        }
        
        [Fact]
        public void CreateProductWithInvalidDescription()
        {
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), "Test", string.Empty, "Test", 1, 1, new Dimensions(1, 1, 1), new Category(Guid.NewGuid(), 1, "Test")));

            exception.Message.Should().Be("Description can't be empty.");
        }

        [Fact]
        public void CreateProductWithInvalidImage()
        {
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), "Test", "Test", string.Empty, 1, 1, new Dimensions(1, 1, 1), new Category(Guid.NewGuid(), 1, "Test")));

            exception.Message.Should().Be("Image can't be empty.");
        }

        [Fact]
        public void CreateProductWithInvalidPrice()
        {
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), "Test", "Test", "Test", 0, 1, new Dimensions(1, 1, 1), new Category(Guid.NewGuid(), 1, "Test")));

            exception.Message.Should().Be("Price must to be greater than zero.");
        }

        [Fact]
        public void CreateProductWithInvalidQuantity()
        {
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), "Test", "Test", "Test", 1, -1, new Dimensions(1, 1, 1), new Category(Guid.NewGuid(), 1, "Test")));

            exception.Message.Should().Be("Quantity must to be greater than zero.");
        }

        [Fact]
        public void CreateProductWithInvalidCategory()
        {
            var exception = Assert.Throws<DomainException>(() => new Product(Guid.NewGuid(), "Test", "Test", "Test", 1, 1, new Dimensions(1, 1, 1), null!));

            exception.Message.Should().Be("Category can't be empty.");
        }

        [Fact]
        public void CreateValidProduct()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

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
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            product.Deactivate();
            product.Active.Should().Be(false);

            product.Activate();
            product.Active.Should().Be(true);
        }

        [Fact]
        public void ChangeDescriptionUsingInvalidDescription()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            var exception = Assert.Throws<DomainException>(() => product.ChangeDescription(string.Empty));

            exception.Message.Should().Be("Description can't be empty.");
        }

        [Fact]
        public void ChangeDescriptionUsingValidDescription()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            product.ChangeDescription("New description");

            product.Description.Should().Be("New description");
        }

        [Fact]
        public void ChangeCategoryUsingNullCategory()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            var exception = Assert.Throws<DomainException>(() => product.ChangeCategory(null!));

            exception.Message.Should().Be("Category can't be empty.");
        }

        [Fact]
        public void ChangeCategoryUsingValidCategory()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            product.ChangeCategory(new Category(Guid.NewGuid(), 7, "New category"));

            product.Category.Code.Should().Be(7);
            product.Category.Name.Should().Be("New category");
        }

        [Fact]
        public void CompareProdutcsWithDifferentIds()
        {
            var product1 = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));            
            product1.ChangeCategory(new Category(Guid.NewGuid(), 7, "New category"));
            product1.Category.Code.Should().Be(7);
            product1.Category.Name.Should().Be("New category");

            var product2 = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));
            product2.ChangeCategory(new Category(Guid.NewGuid(), 7, "New category"));
            product2.Category.Code.Should().Be(7);
            product2.Category.Name.Should().Be("New category");

            product1.Equals(product2).Should().BeFalse();
        }

        [Fact]
        public void CompareProdutcsWithItself()
        {
            var product1 = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));
            product1.ChangeCategory(new Category(Guid.NewGuid(), 7, "New category"));
            product1.Category.Code.Should().Be(7);
            product1.Category.Name.Should().Be("New category");            

            product1.Equals(product1).Should().BeTrue();
        }

        [Fact]
        public void GetHashCodeThrowsException()
        {
            var product = new Product(Guid.NewGuid(), "Name", "Description", "Image", 1, 2, new Dimensions(3, 4, 5), new Category(Guid.NewGuid(), 6, "Test"));

            Assert.Throws<NotImplementedException>(() => product.GetHashCode());
        }
    }
}
