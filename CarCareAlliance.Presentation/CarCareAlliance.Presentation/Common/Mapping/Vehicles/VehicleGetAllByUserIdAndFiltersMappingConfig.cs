using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Vehicles.Common;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Vehicles
{
    public class VehicleGetAllByUserIdAndFiltersMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PagedResult<VehicleGetResult>, PagedResponse<VehicleDto>>()
                .Map(dest => dest.Data, src => src.Data.Select(result => 
                    new VehicleDto(
                        result.Vehicle.Id.Value,
                        result.Vehicle.Brand,
                        result.Vehicle.Model,
                        result.Vehicle.Year,
                        result.Vehicle.Vin,
                        result.Vehicle.LicensePlate,
                        result.Vehicle.UserProfileId.Value)).ToList());
        }
    }
}
