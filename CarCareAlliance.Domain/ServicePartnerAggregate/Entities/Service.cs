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
        
        private Service(
            ServiceId id,
            string name,
            string description,
            float price,
            float duration) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Duration = duration;
        }

        public static Service Create(
            string name,
            string description,
            float price,
            float duration)
        {
            return new(
                ServiceId.CreateUnique(),
                name,
                description,
                price,
                duration);
        }
    }
}