using FluentAssertions;
using System;
using Xunit;

namespace NerdStore.Catalog.Domain.Test
{
    public class CategoryTest
    {
        [Fact]
        public void CreateCategoryWithInvalidCode()
        {            
            var exception = Assert.Throws<Exception>(() => new Category(0, "Test"));

            exception.Message.Should().Be("Code must be greater than zero.");
        }

        [Fact]
        public void CreateCategoryWithInvalidName()
        {
            var exception = Assert.Throws<Exception>(() => new Category(1, string.Empty));

            exception.Message.Should().Be("Name can't be empty.");
        }        

        [Fact]
        public void CreateValidDimensions()
        {
            var category = new Category(1, "Test");

            category.Code.Should().Be(1);
            category.Name.Should().Be("Test");
        }
    }
}
