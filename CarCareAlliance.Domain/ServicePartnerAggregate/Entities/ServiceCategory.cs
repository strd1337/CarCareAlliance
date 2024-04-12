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
            string description,
            List<Service> services) : base(id)
        {
            Name = name;
            Description = description;
            this.services = services;
        }

        public static ServiceCategory Create(
            string name,
            string description,
            List<Service> services)
        {
            return new(
                ServiceCategoryId.CreateUnique(),
                name,
                description,
                services);
        }

        public void AddService(Service service)
        {
            services.Add(service);
        }

        public void Update(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }

#pragma warning disable CS8618
        public ServiceCategory()
        {
        }
#pragma warning restore CS8618
    }
}