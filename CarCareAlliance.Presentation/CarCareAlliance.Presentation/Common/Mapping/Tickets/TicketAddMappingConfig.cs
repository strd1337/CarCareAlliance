using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Create;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Tickets
{
    public class TicketAddMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TicketCreateResult, TicketCreateResponse>()
                .Map(dest => dest.Ticket, src => new TicketDto(
                    src.Ticket.Id.Value,
                    src.Ticket.Description,
                    src.Ticket.DateSubmitted,
                    src.Ticket.RepairStatus,
                    src.Ticket.PaymentStatus,
                    src.Ticket.UserProfileId.Value,
                    src.Ticket.VehicleId.Value,
                    new OrderDetailsDto(
                        src.Ticket.OrderDetails.Id.Value,
                        src.Ticket.OrderDetails.Mileage,
                        src.Ticket.OrderDetails.Comments,
                        src.Ticket.OrderDetails.FinalPrice,
                        src.Ticket.OrderDetails.PrepaymentAmount,
                        src.Ticket.OrderDetails.ServiceIds.Select(x => x.Value).ToList())));
        }
    }
}
