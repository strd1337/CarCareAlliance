using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Tickets
{
    public class TicketGetAllByFiltersConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PagedResult<TicketDto>, PagedResponse<TicketDto>>();
        }
    }
}
