using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Contracts.Tickets.Get;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Tickets
{
    public class TicketGetByUserIdConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TicketGetAllByUserIdResult, TicketGetAllByUserIdResponse>()
                .Map(dest => dest.Tickets, src => src.Tickets.Select(ticket =>
                    new TicketDto(
                        ticket.Id.Value,
                        ticket.Description,
                        ticket.DateSubmitted,
                        ticket.RepairStatus,
                        ticket.PaymentStatus,
                        ticket.UserProfileId.Value,
                        ticket.VehicleId.Value,
                        new OrderDetailsDto(
                            ticket.OrderDetails.Id.Value,
                            ticket.OrderDetails.Mileage,
                            ticket.OrderDetails.Comments,
                            ticket.OrderDetails.FinalPrice,
                            ticket.OrderDetails.PrepaymentAmount,
                            ticket.OrderDetails.ServiceIds.Select(x => x.Value).ToList()))).ToList());
        }
    }
}
