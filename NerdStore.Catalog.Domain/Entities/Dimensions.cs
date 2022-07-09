using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalog.Domain.Entities
{
    public class Dimensions
    {
        public decimal Height { get; private set; }
        public decimal Width { get; private set; }
        public decimal Depth { get; private set; }

        public Dimensions()
        {
            Height = 0;
            Width = 0;
            Depth = 0;
        }

        public Dimensions(decimal height, decimal width, decimal depth)
        {
            Height = height;
            Width = width;
            Depth = depth;

            Validate();
        }

        public void Validate()
        {
            Height.ExceptionIfLessThan(1, "Height cant't be less than one.");
            Width.ExceptionIfLessThan(1, "Width cant't be less than one.");
            Depth.ExceptionIfLessThan(1, "Depth cant't be less than one.");
        }

        public override bool Equals(object? obj)
        {
            var dimensions = (Dimensions)obj!;
            
            return dimensions.Height == Height && dimensions.Width == Width && dimensions.Depth == Depth;
        }
    }
}