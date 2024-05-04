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
                .Map(dest => dest.Ticket, src => new TicketDto
                {
                    TicketId = src.Ticket.Id.Value,
                    Description = src.Ticket.Description,
                    DateSubmitted = src.Ticket.DateSubmitted,
                    RepairStatus = src.Ticket.RepairStatus,
                    PaymentStatus = src.Ticket.PaymentStatus,
                    UserProfileId = src.Ticket.UserProfileId.Value,
                    VehicleId = src.Ticket.VehicleId.Value,
                    OrderDetails = new OrderDetailsDto
                    {
                        OrderDetailsId = src.Ticket.OrderDetails.Id.Value,
                        Mileage = src.Ticket.OrderDetails.Mileage,
                        Comments = src.Ticket.OrderDetails.Comments,
                        FinalPrice = src.Ticket.OrderDetails.FinalPrice,
                        PrepaymentAmount = src.Ticket.OrderDetails.PrepaymentAmount,
                        ServiceIds = src.Ticket.OrderDetails.ServiceIds.Select(x => x.Value).ToList()
                    }
                });
        }
    }
}
