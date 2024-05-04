using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Contracts.ServicePartners.Common;
using CarCareAlliance.Contracts.Tickets.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Application.Tickets.Queries.GetAllByFilters
{
    public class TicketGetAllByFiltersHandler(
        IUnitOfWork unitOfWork) : IQueryHandler<TicketGetAllByFiltersQuery, PagedResult<TicketDto>>
    {
        public async Task<ErrorOr<PagedResult<TicketDto>>> Handle(
            TicketGetAllByFiltersQuery query,
            CancellationToken cancellationToken)
        {
            var allServiceHistories = unitOfWork
                .GetRepository<ServiceHistory, ServiceHistoryId>()
                .GetAll([nameof(ServiceHistory.Tickets),
                    nameof(ServiceHistory.Tickets) + "." + nameof(Ticket.OrderDetails)]);

            var user = await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(UserProfileId.Create(query.UserId), cancellationToken);

            IQueryable<ServiceHistory> serviceHistories;
            if (user is null)
            {
                var mechanic = await unitOfWork
                    .GetRepository<MechanicProfile, MechanicProfileId>()
                    .GetByIdAsync(MechanicProfileId.Create(query.UserId), cancellationToken);

                if (mechanic is null)
                {
                    return Errors.UserProfile.NotFound;
                }

                serviceHistories = allServiceHistories
                  .Where(sh => sh.Tickets.Any(t => t.OrderDetails.AssignedMechanicId == mechanic.Id))
                    .AsNoTracking();
            }
            else
            {
                serviceHistories = allServiceHistories
                    .Where(sh => sh.Tickets.Any(t => t.UserProfileId == UserProfileId.Create(query.UserId)))
                    .AsNoTracking();
            }

            var tickets = serviceHistories.SelectMany(sh => sh.Tickets);

            if (query.RepairStatus is not null)
            {
                tickets = tickets.Where(x => x.RepairStatus == query.RepairStatus);
            }

            var resultTickets = new List<TicketDto>();
            foreach(var ticket in tickets)
            {
                var vehicle = await unitOfWork
                    .GetRepository<Vehicle, VehicleId>()
                    .GetByIdAsync(VehicleId.Create(ticket.VehicleId.Value), cancellationToken);

                if (vehicle is null)
                {
                    return Errors.Vehicle.NotFound;
                }

                var serviceHistory = serviceHistories.FirstOrDefault(sh => sh.Tickets.Any(t => t.Id == ticket.Id));
                
                if (serviceHistory is null)
                {
                    return Errors.ServiceHistory.NotFound;
                }

                if (serviceHistory is null)
                {
                    return Errors.ServiceHistory.NotFound;
                }

                var servicePartner = await unitOfWork
                    .GetRepository<ServicePartner, ServicePartnerId>()
                    .GetByIdAsync(
                        ServicePartnerId.Create(serviceHistory.ServicePartnerId.Value),
                        cancellationToken);

                if (servicePartner is null)
                {
                    return Errors.ServicePartner.NotFound;
                }

                var ticketServiceIds = ticket.OrderDetails.ServiceIds.ToList();

                var services = servicePartner.ServiceCategories
                    .SelectMany(sc => sc.Services)
                    .Where(s => ticketServiceIds.Contains(s.Id))
                    .Select(s => new ServiceDto(
                        s.Id.Value,
                        s.Name,
                        s.Description,
                        s.Price,
                        s.Duration))
                    .ToList();

                var orderDetails = new OrderDetailsDto
                {
                    OrderDetailsId = ticket.OrderDetails.Id.Value,
                    Mileage = ticket.OrderDetails.Mileage,
                    Comments = ticket.OrderDetails.Comments,
                    FinalPrice = ticket.OrderDetails.FinalPrice,
                    PrepaymentAmount = ticket.OrderDetails.PrepaymentAmount,
                    Services = services
                };

                resultTickets.Add(new TicketDto
                {
                    TicketId = ticket.Id.Value,
                    Description = ticket.Description,
                    DateSubmitted = ticket.DateSubmitted,
                    RepairStatus = ticket.RepairStatus,
                    PaymentStatus = ticket.PaymentStatus,
                    ServicePartnerName = servicePartner.Name,
                    VehicleVin = vehicle.Vin,
                    OrderDetails = orderDetails,
                });
            }

            int resultsCount = resultTickets.Count;

            var result = resultTickets.OrderBy(x => x.DateSubmitted)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            var pagedResult = new PagedResult<TicketDto>(
                query.PageNumber,
                query.PageSize,
                resultsCount,
                result
            );

            return pagedResult;
        }
    }
}
