using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Category : Entity
    {
        public int Code { get; private set; }
        public string Name { get; private set; }

        public Category(int code, string name)
        {
            Code = code;
            Name = name;

            Validate();
        }

        public void Validate()
        {
            Code.ExceptionIfEqualOrLessThan(0, "Code must be greater than zero.");
            Name.ExceptionIfEmpty("Name can't be empty.");
        }
    }
}