using FluentAssertions;
using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Core.Test.DomainObjects
{
    public class EntityTest
    {
        public class EntityDummy : Entity { }

        [Fact]
        public void CreateEntity()
        { 
            var entity = new EntityDummy();

            entity.Id.Should().NotBe(Guid.Empty);
        }
    }
}
