using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Vehicles.Common;

namespace CarCareAlliance.Application.Vehicles.Commands
{
    public record VehicleAddCommand(
        string Brand,
        string Model,
        int Year,
        string Vin,
        string LicensePlate,
        Guid UserProfileId) : ICommand<VehicleAddResult>;
}