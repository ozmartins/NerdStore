using FluentAssertions;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Catalog.Domain.Test.Entities
{
    public class DimensionsTest
    {
        [Fact]
        public void CreateDimensionsWithInvalidHeight()
        {            
            var exception = Assert.Throws<DomainException>(() => new Dimensions(0, 1, 1));

            exception.Message.Should().Be("Height cant't be less than one.");
        }

        [Fact]
        public void CreateDimensionsWithInvalidWidth()
        {
            var exception = Assert.Throws<DomainException>(() => new Dimensions(1, 0, 1));

            exception.Message.Should().Be("Width cant't be less than one.");
        }

        [Fact]
        public void CreateDimensionsWithInvalidDepth()
        {
            var exception = Assert.Throws<DomainException>(() => new Dimensions(1, 1, 0));

            exception.Message.Should().Be("Depth cant't be less than one.");
        }

        [Fact]
        public void CreateValidDimensions()
        {
            var dimension = new Dimensions(1, 2, 3);

            dimension.Height.Should().Be(1);
            dimension.Width.Should().Be(2);
            dimension.Depth.Should().Be(3);
        }
    }
}
