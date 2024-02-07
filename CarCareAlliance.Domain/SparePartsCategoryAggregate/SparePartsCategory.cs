using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.PartsCategoryAggregate.ValueObjects;

namespace CarCareAlliance.Domain.PartsCategoryAggregate
{
    public sealed class SparePartsCategory : AggregateRoot<SparePartsCategoryId, Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private SparePartsCategory(
            SparePartsCategoryId id,
            string name,
            string description) : base(id)
        {
            Name = name;
            Description = description;
        }
        
        public static SparePartsCategory Create(
            string name,
            string description)
        {
            return new SparePartsCategory(
                SparePartsCategoryId.CreateUnique(),
                name,
                description);
        }

#pragma warning disable CS8618
        private SparePartsCategory()
        {
        }
#pragma warning restore CS8618
    }
}