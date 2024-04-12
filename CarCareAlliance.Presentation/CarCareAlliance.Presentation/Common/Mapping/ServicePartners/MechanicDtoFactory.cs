using CarCareAlliance.Contracts.Staff.Common;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;

namespace CarCareAlliance.Presentation.Common.Mapping.ServicePartners
{
    public static class MechanicDtoFactory
    {
        public static MechanicDto CreateMechanicDto(
            MechanicProfile mechanic, 
            UserProfile mechanicProfile)
        {
            return new MechanicDto(
                mechanic.Id.Value,
                mechanicProfile.Id.Value,
                mechanicProfile?.Information?.FirstName ?? "",
                mechanicProfile?.Information?.LastName ?? "",
                mechanicProfile?.Information?.PhoneNumber ?? "",
                mechanicProfile?.Information?.Country ?? "",
                mechanicProfile?.Information?.City ?? "",
                mechanic.Experience);
        }
    }
}
