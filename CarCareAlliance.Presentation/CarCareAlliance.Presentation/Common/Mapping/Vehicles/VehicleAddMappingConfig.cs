using CarCareAlliance.Application.Vehicles.Commands;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Contracts.Vehicles.Add;
using CarCareAlliance.Contracts.Vehicles.Common;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Vehicles
{
    public class VehicleAddMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleAddRequest, VehicleAddCommand>()
                .Map(dest => dest, src => src);

            config.NewConfig<VehicleAddResult, VehicleAddResponse>()
                .Map(dest => dest.Vehicle, src => new VehicleDto(
                     src.Vehicle.Id.Value,
                     src.Vehicle.Brand,
                     src.Vehicle.Model,
                     src.Vehicle.Year,
                     src.Vehicle.Vin,
                     src.Vehicle.LicensePlate,
                     src.Vehicle.UserProfileId.Value));
        }
    }
}