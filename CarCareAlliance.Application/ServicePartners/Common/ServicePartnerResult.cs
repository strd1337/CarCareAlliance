using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.WorkScheduleAggregate;

namespace CarCareAlliance.Application.ServicePartners.Common
{
    public class ServicePartnerResult
    {
        public ServicePartner ServicePartner { get; set; } = null!;
        public ICollection<MechanicProfile> Mechanics { get; set; } = null!;
        public ICollection<UserProfile> MechanicProfiles { get; set; } = null!;
        public ICollection<WorkSchedule> WorkSchedules { get; set; } = null!;
    }
}