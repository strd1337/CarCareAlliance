using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;

namespace CarCareAlliance.Application.Staff.Common
{
    public class GetAllStaffsByFiltersResult
    {
        public MechanicProfile Mechanic { get; set; } = null!;
        public UserProfile MechanicProfile { get; set; } = null!;
    }
}
