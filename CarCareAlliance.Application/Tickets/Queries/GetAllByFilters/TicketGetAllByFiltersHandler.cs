using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.Tickets.Queries.GetAllByFilters
{
    public class TicketGetAllByFiltersHandler(
        IUnitOfWork unitOfWork) : IQueryHandler<TicketGetAllByFiltersQuery, PagedResult<Ticket>>
    {
        public async Task<ErrorOr<PagedResult<Ticket>>> Handle(
            TicketGetAllByFiltersQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServiceHistory, ServiceHistoryId>()
                .FirstOrDefaultAsync(
                    x => x.ServicePartnerId == ServicePartnerId.Create(query.ServicePartnerId),
                    cancellationToken,
                    [nameof(ServiceHistory.Tickets),
                     nameof(ServiceHistory.Tickets) + "." + nameof(Ticket.OrderDetails)]);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

            IEnumerable<Ticket> tickets = servicePartner.Tickets;

            if (query.RepairStatus is not null)
            {
                tickets = tickets.Where(x => x.RepairStatus == query.RepairStatus);
            }

            if (query.DateTime is not null)
            {
                tickets = tickets.Where(x => x.DateSubmitted == query.DateTime);
            }

            int resultsCount = tickets.Count();

            var result = tickets.ToList().OrderBy(x => x.DateSubmitted)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            
            var pagedResult = new PagedResult<Ticket>(
                query.PageNumber,
                query.PageSize,
                resultsCount,
                result
            );

            return pagedResult;
        }
    }
}
