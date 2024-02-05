using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.Entities
{
    public sealed class Service : Entity<ServiceId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float Price { get; private set; }
        public float Duration { get; private set; }
        public ServiceCategoryId ServiceCategoryId { get; private set; }
        
        private Service(
            ServiceId id,
            string name,
            string description,
            float price,
            float duration,
            ServiceCategoryId serviceCategoryId) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
            ServiceCategoryId = serviceCategoryId;
        }

        public static Service Create(
            string name,
            string description,
            float price,
            float duration,
            ServiceCategoryId serviceCategoryId)
        {
            return new(
                ServiceId.CreateUnique(),
                name,
                description,
                price,
                duration,
                serviceCategoryId);
        }
    }
}
