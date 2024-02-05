using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.ReviewAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;

namespace CarCareAlliance.Domain.ServicePartnerAggregate
{
    public sealed class ServicePartner : AggregateRoot<ServicePartnerId, Guid>
    {
        private readonly List<ServiceCategoryId> serviceCategoryIds = [];
        private readonly List<PhotoId> photoIds = [];
        private readonly List<ReviewId> reviewIds = [];
        private readonly List<MechanicProfileId> mechanicProfileIds = [];
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public PhotoId LogoId { get; private set; }
        public WorkScheduleId WorkScheduleId { get; private set; }
        public ServiceLocationId ServiceLocationId { get; private set; }

        private ServicePartner(
            ServicePartnerId id,
            string name,
            string description,
            PhotoId logoId,
            WorkScheduleId workScheduleId,
            ServiceLocationId serviceLocationId) : base(id)
        {
            Name = name;
            Description = description;
            LogoId = logoId;
            WorkScheduleId = workScheduleId;
            ServiceLocationId = serviceLocationId;
        }

        public static ServicePartner Create(
            string name,
            string description,
            PhotoId logoId,
            WorkScheduleId workScheduleId,
            ServiceLocationId serviceLocationId)
        {
            return new ServicePartner(
                ServicePartnerId.CreateUnique(),
                name,
                description,
                logoId,
                workScheduleId,
                serviceLocationId);
        }

#pragma warning disable CS8618
        private ServicePartner()
        {
        }
#pragma warning restore CS8618
    }
}
