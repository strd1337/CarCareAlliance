using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Contracts.Vehicles.Common;
using CarCareAlliance.Contracts.Vehicles.Get;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Vehicles
{
    public class VehicleGetMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleGetResult, VehicleGetResponse>()
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
