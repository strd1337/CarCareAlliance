using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Staff.Common;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Staff.Common;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Staff
{
    public class StaffsGetAllByFiltersMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PagedResult<GetAllStaffsByFiltersResult>, PagedResponse<MechanicDto>>()
                .Map(dest => dest.Data, src => src.Data.Select(result => new MechanicDto(
                    result.Mechanic.Id.Value,
                    result.MechanicProfile.Id.Value,
                    result.MechanicProfile.Information.FirstName ?? "",
                    result.MechanicProfile.Information.LastName ?? "",
                    result.MechanicProfile.Information.PhoneNumber ?? "",
                    result.MechanicProfile.Information.Country ?? "",
                    result.MechanicProfile.Information.City ?? "",
                    result.Mechanic.Experience)));
        }
    }
}
