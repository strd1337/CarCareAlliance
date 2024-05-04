using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Tickets.Common;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Entities;
using CarCareAlliance.Domain.TicketAggregate.Enums;
using CarCareAlliance.Domain.ServiceHistoryAggregate.Enums;
using CarCareAlliance.Domain.TicketAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate;
using CarCareAlliance.Domain.ServiceHistoryAggregate.ValueObjects;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;

namespace CarCareAlliance.Application.Tickets.Commands.Create
{
    public class TicketCreateHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<TicketCreateCommand, TicketCreateResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<TicketCreateResult>> Handle(
            TicketCreateCommand command,
            CancellationToken cancellationToken)
        {
            var user = await unitOfWork
               .GetRepository<UserProfile, UserProfileId>()
               .GetByIdAsync(
                   UserProfileId.Create(command.UserProfileId),
                   cancellationToken);

            if (user is null)
            {
                return Errors.UserProfile.NotFound;
            }

            var vehicle = await unitOfWork
               .GetRepository<Vehicle, VehicleId>()
               .GetByIdAsync(
                   VehicleId.Create(command.VehicleId),
                   cancellationToken);

            if (vehicle is null)
            {
                return Errors.Vehicle.NotFound;
            }

            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .FirstOrDefaultAsync(
                    x => x.Id == ServicePartnerId.Create(command.ServicePartnerId),
                    cancellationToken);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

            var services = servicePartner
                .ServiceCategories
                .SelectMany(sc => sc.Services)
                .Where(s => command.ServiceIds.Distinct().ToList().Contains(s.Id.Value))
                .ToList();

            if (services.Count != command.ServiceIds.Count)
            {
                return Errors.Service.ServicesNotFound;
            }

            float prepaymentAmount = services.Sum(s => s.Price);
            float finalPrice = prepaymentAmount;

            var orderDetails = OrderDetails.Create(
                command.Mileage,
                command.OrderDetailsComments,
                finalPrice,
                prepaymentAmount);

            orderDetails.UpdateAssignedMechanic(MechanicProfileId.Create(command.AssignedMechanicId));

            foreach (var service in services)
            {
                orderDetails.AddService(ServiceId.Create(service.Id.Value));
            }

            var ticket = Ticket.Create(
                command.TicketDescription,
                DateTime.UtcNow,
                RepairStatus.Pending,
                PaymentStatus.Unpaid,
                UserProfileId.Create(user.Id.Value),
                VehicleId.Create(vehicle.Id.Value),
                orderDetails);

            var serviceHistory = await unitOfWork
                .GetRepository<ServiceHistory, ServiceHistoryId>()
                .FirstOrDefaultAsync(sh => sh.ServicePartnerId == ServicePartnerId.Create(command.ServicePartnerId), cancellationToken);

            if (serviceHistory is null)
            {
                serviceHistory = ServiceHistory.Create(
                    ServicePartnerId.Create(servicePartner.Id.Value));

                serviceHistory.AddTicket(ticket);

                await unitOfWork
                    .GetRepository<ServiceHistory, ServiceHistoryId>()
                    .AddAsync(serviceHistory, cancellationToken);
            }
            else
            {
                serviceHistory.AddTicket(ticket);

                await unitOfWork
                    .GetRepository<ServiceHistory, ServiceHistoryId>()
                    .UpdateAsync(serviceHistory);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TicketCreateResult(ticket);
        }
    }
}