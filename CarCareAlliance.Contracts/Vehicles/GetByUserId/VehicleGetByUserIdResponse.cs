using CarCareAlliance.Contracts.Vehicles.Common;

namespace CarCareAlliance.Contracts.Vehicles.GetByUserId
{
    public record VehicleGetByUserIdResponse(
        ICollection<VehicleDto> Vehicles);
}
