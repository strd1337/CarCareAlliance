using CarCareAlliance.Domain.VehicleAggregate;

namespace CarCareAlliance.Application.Vehicles.Common
{
    public record VehicleGetByUserIdResult(
        ICollection<Vehicle> Vehicles);
}
