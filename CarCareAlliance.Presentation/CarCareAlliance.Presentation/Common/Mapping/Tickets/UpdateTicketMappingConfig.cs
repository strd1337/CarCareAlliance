using CarCareAlliance.Application.Tickets.Commands.Update;
using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Update;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Tickets
{
    public class UpdateTicketMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateTicketRequest, UpdateTicketCommand>();

            config.NewConfig<UpdateTicketResult, UpdateTicketResponse>();
        }
    }
}
