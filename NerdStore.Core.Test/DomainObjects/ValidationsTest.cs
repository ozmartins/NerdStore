using FluentAssertions;
using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Core.Test.DomainObjects
{
    public class ValidationsTest
    {
        [Fact]
        public void WhenCallExceptionIfNullUsingNullObjectShouldThrownException()
        {
            object object1 = null!;

            var exception = Assert.Throws<Exception>(() => object1.ExceptionIfNull("TestException"));

            exception.Message.Should().Be("TestException");
        }
        
        [Fact]
        public void WhenCallExceptionIfNullUsingNonNullObjectShouldntThrownException()
        {
            new object().Invoking(x => x.ExceptionIfNull("TestException")).Should().NotThrow();
        }

        [Fact]
        public void WhenCallExceptionIfEmptyUsingEmptyStringShouldThrownException()
        {            
            var exception = Assert.Throws<Exception>(() => string.Empty.ExceptionIfEmpty("TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfEmptyUsingNonEmptyStringShouldntThrownException()
        {
            "Test".Invoking(x => x.ExceptionIfEmpty("TestException")).Should().NotThrow();
        }

        [Fact]
        public void WhenCallExceptionIfEmptyUsingEmptyGuidShouldThrownException()
        {            
            var exception = Assert.Throws<Exception>(() => Guid.Empty.ExceptionIfEmpty("TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfEmptyUsingNonEmptyGuidShouldntThrownException()
        {
            Guid.NewGuid().Invoking(x => x.ExceptionIfEmpty("TestException")).Should().NotThrow();
        }

        [Fact]
        public void WhenCallExceptionIfLessThanUsingInvalidIntegerShouldThrownException()
        {
            var number = 0;

            var exception = Assert.Throws<Exception>(() => number.ExceptionIfLessThan(1, "TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfLessThanUsingValidaIntegerShouldntThrownException()
        {
            var number = 1;

            number.Invoking(x => x.ExceptionIfLessThan(1, "TestException")).Should().NotThrow();
        }

        [Fact]
        public void WhenCallExceptionIfLessThanUsingInvalidDecimalShouldThrownException()
        {
            var number = 0.0m;

            var exception = Assert.Throws<Exception>(() => number.ExceptionIfLessThan(0.1m, "TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfLessThanUsingValidaDecimalShouldntThrownException()
        {
            var number = 0.1m;

            number.Invoking(x => x.ExceptionIfLessThan(0.1m, "TestException")).Should().NotThrow();
        }

        [Fact]
        public void WhenCallExceptionIfEqualOrLessThanUsingAnEqualntegerShouldThrownException()
        {
            var number = 1;

            var exception = Assert.Throws<Exception>(() => number.ExceptionIfEqualOrLessThan(1, "TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfEqualOrLessThanUsingAnLowerIntegerShouldThrownException()
        {
            var number = 0;

            var exception = Assert.Throws<Exception>(() => number.ExceptionIfEqualOrLessThan(1, "TestException"));

            exception.Message.Should().Be("TestException");
        }

        [Fact]
        public void WhenCallExceptionIfEqualOrLessThanUsingBiggerIntegerShouldntThrownException()
        {
            var number = 2;

            number.Invoking(x => x.ExceptionIfEqualOrLessThan(1, "TestException")).Should().NotThrow();
        }

    }
}
