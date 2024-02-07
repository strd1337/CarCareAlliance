using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MaintenanceAggregate.ValueObjects;

namespace CarCareAlliance.Domain.MaintenanceAggregate.Entities
{
    public sealed class MaintenanceType : Entity<MaintenanceTypeId>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        private MaintenanceType(
            MaintenanceTypeId id,
            string name,
            string description) : base(id)
        {
            Name = name;
            Description = description;
        }

        public static MaintenanceType Create(
            string name,
            string description)
        {
            return new(
                MaintenanceTypeId.CreateUnique(),
                name,
                description);
        }
    }
}
