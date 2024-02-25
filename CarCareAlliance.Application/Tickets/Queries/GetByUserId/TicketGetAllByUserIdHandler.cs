using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Tickets.Common;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;

namespace CarCareAlliance.Application.Tickets.Queries.GetByUserId
{
    public class TicketGetAllByUserIdHandler(
        IUnitOfWork unitOfWork) :
        IQueryHandler<TicketGetAllByUserIdQuery, TicketGetAllByUserIdResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<TicketGetAllByUserIdResult>> Handle(
            TicketGetAllByUserIdQuery query,
            CancellationToken cancellationToken)
        {
            var serviceHistory = await unitOfWork
               .GetRepository<ServiceHistory, ServiceHistoryId>()
               .FirstOrDefaultAsync(
                    x => x.ServicePartnerId == ServicePartnerId.Create(query.ServicePartnerId),
                    cancellationToken,
                    [nameof(ServiceHistory.Tickets),
                        nameof(ServiceHistory.Tickets) + "." + nameof(Ticket.OrderDetails)]);

            if (serviceHistory is null)
            {
                return Errors.ServiceHistory.NotFound;
            }
           
            var tickets = serviceHistory
                    .Tickets
                    .Where(t => t.UserProfileId == UserProfileId.Create(query.UserId))
                    .ToList();

            if (tickets.Count == 0)
            {
                return Errors.ServiceHistory.NotFound;
            }

            return new TicketGetAllByUserIdResult(tickets);
        }
    }
}
