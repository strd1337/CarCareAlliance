using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.TicketAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.Tickets.Commands.Update
{
    public class UpdateTicketHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<UpdateTicketCommand, UpdateTicketResult>
    {
        public async Task<ErrorOr<UpdateTicketResult>> Handle(
            UpdateTicketCommand command, 
            CancellationToken cancellationToken)
        {
            var serviceHistory = unitOfWork
                .GetRepository<ServiceHistory, ServiceHistoryId>()
                .GetAll([nameof(ServiceHistory.Tickets),
                    nameof(ServiceHistory.Tickets) + "." + nameof(Ticket.OrderDetails)])
                .AsEnumerable()
                .FirstOrDefault(sh => sh.Tickets.Any(t => TicketId.Create(t.Id.Value) == TicketId.Create(command.TicketId)));

            if (serviceHistory is null)
            {
                return Errors.ServiceHistory.NotFound;
            }

            var ticket = serviceHistory.Tickets.FirstOrDefault(t => TicketId.Create(t.Id.Value) == TicketId.Create(command.TicketId));

            if (ticket is null)
            {
                return Errors.ServiceHistory.TicketNotFound;
            }

            ticket.UpdateRepairStatus(command.RepairStatus);

            ticket.OrderDetails.UpdateComments(command.Comments);

            await unitOfWork
                .GetRepository<ServiceHistory, ServiceHistoryId>()
                .UpdateAsync(serviceHistory);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdateTicketResult();
        }
    }
}
