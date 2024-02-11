using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using ErrorOr;
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
                location);

            await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .AddAsync(servicePartner, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServicePartnerAddResult(servicePartner);
        }
    }
}