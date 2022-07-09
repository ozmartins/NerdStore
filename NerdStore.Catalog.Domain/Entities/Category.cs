using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Category : Entity
    {
        public int Code { get; private set; }
        public string Name { get; private set; }

        public Category() 
        {
            Code = 0;
            Name = string.Empty;
        }

        public Category(Guid id, int code, string name)
        {
            Id = id;
            Code = code;
            Name = name;

            Validate();
        }

        public void Validate()
        {
            Id.ExceptionIfEmpty("ID can't be empty.");
            Code.ExceptionIfEqualOrLessThan(0, "Code must be greater than zero.");
            Name.ExceptionIfEmpty("Name can't be empty.");
        }

        public override bool Equals(object? obj)
        {
            var category = (Category)obj!;

            return category.Id == Id && category.Code == Code && category.Name.Equals(Name);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}