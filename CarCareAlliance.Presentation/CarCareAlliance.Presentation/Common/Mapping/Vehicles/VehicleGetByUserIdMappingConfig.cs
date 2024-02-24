using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.ServicePartners.GetAll;
using CarCareAlliance.Contracts.Vehicles.Common;
using CarCareAlliance.Contracts.Vehicles.GetByUserId;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Vehicles
{
    public class VehicleGetByUserIdMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleGetByUserIdResult, VehicleGetByUserIdResponse>()
                .Map(dest => dest.Vehicles, src => src.Vehicles
                    .Select(vehicle =>
                        new VehicleDto(
                            vehicle.Id.Value,
                            vehicle.Brand,
                            vehicle.Model,
                            vehicle.Year,
                            vehicle.Vin,
                            vehicle.LicensePlate,
                            vehicle.UserProfileId.Value)).ToList());
        }
    }
}
