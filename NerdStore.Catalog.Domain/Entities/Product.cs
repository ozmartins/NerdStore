using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }        
        public string Image { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }        
        public int Quantity { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Dimensions Dimensions { get; set; }
        public Category Category { get; set; }
        
        public Product(Guid id, string name, string description, string image, decimal price, int quantityInStock, Dimensions dimensions, Category category)
        {
            Id = id;
            Name = name;
            Description = description;
            Active = true;
            Price = price;
            CreateDate = DateTime.Now;
            Image = image;
            Quantity = quantityInStock;
            Dimensions = dimensions;
            Category = category;
            
            Validate();
        }

        public void Activate() => Active = true;
        
        public void Deactivate() => Active = false;

        public void ChangeCategory(Category category)
        {
            ValidateCategory(category);
            Category = category;
        }

        public void ChangeDescription(string description) 
        {
            description.ExceptionIfEmpty("Description can't be empty.");
            Description = description;
        }
        public void WithdrawFromStock(int quantity)
        {
            ValidateQuantity(quantity);

            if (!HasEnoughtInInventory(quantity)) 
                throw new DomainException("There are not enough items in inventory.");
            
            Quantity -= quantity;
        }

        public void ReplenishStock(int quantity)
        {
            ValidateQuantity(quantity);

            Quantity += quantity;
        }

        public bool HasEnoughtInInventory(int quantity)
        {
            return Quantity >= quantity;
        }

        public void Validate()
        {
            Id.ExceptionIfEmpty("ID can't be empty.");
            Name.ExceptionIfEmpty("Name can't be empty.");
            Image.ExceptionIfEmpty("Image can't be empty.");
            Price.ExceptionIfLessThan(0.01m, "Price must to be greater than zero.");

            ValidateDescription(Description);
            ValidateQuantity(Quantity);
            ValidateCategory(Category);
        }

        public void ValidateDescription(string description)
        {
            description.ExceptionIfEmpty("Description can't be empty.");
        }

        public void ValidateQuantity(int quantity)
        {
            quantity.ExceptionIfLessThan(0, "Quantity must to be greater than zero.");
        }

        public void ValidateCategory(Category category)
        {
            category.ExceptionIfNull("Category can't be empty.");
            category.Id.ExceptionIfEmpty("Category Id can't be empty.");
        }

        public override bool Equals(object? obj)
        {
            var product = (Product)obj!;

            return Id.Equals(product.Id) &&
                   Name.Equals(product.Name) &&
                   Description.Equals(product.Description) &&
                   Image.Equals(product.Image) &&
                   Active == product.Active &&
                   Price == product.Price &&
                   Quantity == product.Quantity &&
                   CreateDate == product.CreateDate &&
                   Dimensions.Equals(product.Dimensions) &&
                   Category.Equals(product.Category);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
