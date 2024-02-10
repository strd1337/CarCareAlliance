using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.PhotoAggregate;
using CarCareAlliance.Domain.PhotoAggregate.ValueObjects;
using CarCareAlliance.Domain.Common.Errors;
using ErrorOr;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;

namespace CarCareAlliance.Application.ServicePartners.Commands.Add
{
    public class ServicePartnerAddHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<ServicePartnerAddCommand, ServicePartnerAddResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerAddResult>> Handle(
            ServicePartnerAddCommand command,
            CancellationToken cancellationToken)
        {
            var logo = await unitOfWork
                .GetRepository<Photo, PhotoId>()
                .GetByIdAsync(
                    PhotoId.Create(command.LogoId),
                    cancellationToken);

            if (logo is null)
            {
                return Errors.Photo.NotFound;
            }

            var workSchedule = await unitOfWork
                .GetRepository<WorkSchedule, WorkScheduleId>()
                .GetByIdAsync(
                    WorkScheduleId.Create(command.WorkScheduleId),
                    cancellationToken);

            if (workSchedule is null)
            {
                return Errors.WorkSchedule.NotFound;
            }

            var location = ServiceLocation.Create(
                command.Latitude,
                command.Longitude,
                command.Address,
                command.City,
                command.Country,
                command.PostalCode,
                command.LocationDescription,
                command.State);

            var servicePartner = ServicePartner.Create(
                command.Name,
                command.Description,
                PhotoId.Create(logo.Id.Value),
                WorkScheduleId.Create(workSchedule.Id.Value),
                location);

            await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .AddAsync(servicePartner, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServicePartnerAddResult(
                servicePartner.Id.Value,
                servicePartner.Name,
                servicePartner.Description,
                servicePartner.LogoId.Value,
                servicePartner.WorkScheduleId.Value);
        }
    }
}