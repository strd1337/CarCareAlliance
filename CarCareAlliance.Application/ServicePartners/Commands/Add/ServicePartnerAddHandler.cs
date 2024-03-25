using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using ErrorOr;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;

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
                command.Location.Latitude,
                command.Location.Longitude,
                command.Location.Address,
                command.Location.City,
                command.Location.Country,
                command.Location.PostalCode,
                command.Location.Description,
                command.Location.State);
            
            var serviceCategories = command.ServiceCategories.Select(serviceCategoryDto =>
                ServiceCategory.Create(
                    serviceCategoryDto.Name,
                    serviceCategoryDto.Description,
                    serviceCategoryDto.Services.Select(serviceDto =>
                        Service.Create(
                            serviceDto.Name,
                            serviceDto.Description,
                            serviceDto.Price,
                            serviceDto.Duration)
                    ).ToList()
                )
            ).ToList();

            var servicePartner = ServicePartner.Create(
                command.Name,
                command.Description,
                location,
                serviceCategories);

            await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .AddAsync(servicePartner, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServicePartnerAddResult(
                servicePartner.Id.Value,
                servicePartner.Name);
        }
    }
}