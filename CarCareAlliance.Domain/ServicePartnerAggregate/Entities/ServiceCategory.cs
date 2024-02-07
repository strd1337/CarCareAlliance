using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServicePartnerAggregate.Entities
{
    public sealed class ServiceCategory : Entity<ServiceCategoryId>
    {
        private readonly List<Service> services = [];
        
        public string Name { get; private set; }
        public string Description { get; private set; }

        public IReadOnlyList<Service> Services => services.AsReadOnly();

        private ServiceCategory(
            ServiceCategoryId id,
            string name,
            string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public static ServiceCategory Create(
            string name,
            string description)
        {
            return new(
                ServiceCategoryId.CreateUnique(),
                name,
                description);
        }

        public void AddService(Service service)
        {
            services.Add(service);
        }
    }
}