using FluentAssertions;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Entities
{
    public class CategoryTest
    {
        [Fact]
        public void CreateCategoryWithInvalidCode()
        {            
            var exception = Assert.Throws<DomainException>(() => new Category(Guid.NewGuid(), 0, "Test"));

            exception.Message.Should().Be("Code must be greater than zero.");
        }

        [Fact]
        public void CreateCategoryWithInvalidName()
        {
            var exception = Assert.Throws<DomainException>(() => new Category(Guid.NewGuid(), 1, string.Empty));

            exception.Message.Should().Be("Name can't be empty.");
        }        

        [Fact]
        public void CreateValidCategory()
        {
            var category = new Category(Guid.NewGuid(), 1, "Test");

            category.Code.Should().Be(1);
            category.Name.Should().Be("Test");
        }

        [Fact]
        public void GetHashCodeThrowsException()
        {
            Assert.Throws<NotImplementedException>(() => new Category().GetHashCode());           
        }
    }
}
