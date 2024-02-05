using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PartsCategoryAggregate.ValueObjects;
using CarCareAlliance.Domain.SparePartAggregate.ValueObjects;

namespace CarCareAlliance.Domain.SparePartAggregate
{
    public sealed class SparePart : AggregateRoot<SparePartId, Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public int StockQuantity { get; private set; }
        public string Manufacturer { get; private set; }
        public SparePartsCategoryId SparePartsCategoryId { get; private set; }

        private SparePart(
            SparePartId id,
            string name,
            string description,
            float price,
            int stockQuantity,
            string manufacturer,
            SparePartsCategoryId sparePartsCategoryId) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Manufacturer = manufacturer;
            SparePartsCategoryId = sparePartsCategoryId;
        }

        public static SparePart Create(
            string name,
            string description,
            float price,
            int stockQuantity,
            string manufacturer,
            SparePartsCategoryId sparePartsCategoryId)
        {
            return new SparePart(
                SparePartId.CreateUnique(),
                name,
                description,
                price,
                stockQuantity,
                manufacturer,
                sparePartsCategoryId);
        }

#pragma warning disable CS8618
        private SparePart()
        {
        }
#pragma warning restore CS8618
    }
}
