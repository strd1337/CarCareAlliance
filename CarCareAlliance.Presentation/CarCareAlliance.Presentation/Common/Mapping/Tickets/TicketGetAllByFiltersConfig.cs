using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Contracts.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using Mapster;

namespace CarCareAlliance.Presentation.Common.Mapping.Tickets
{
    public class TicketGetAllByFiltersConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PagedResult<Ticket>, PagedResponse<TicketDto>>()
                .Map(dest => dest.Data, src => src.Data.Select(ticket =>
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
