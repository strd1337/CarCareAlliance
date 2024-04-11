using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using ErrorOr;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using CarCareAlliance.Domain.Common.Errors;

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

            var services = serviceCategories
                .SelectMany(category => category.Services)
                .ToList();

            var servicePartners = unitOfWork
               .GetRepository<ServicePartner, ServicePartnerId>()
               .GetAll([nameof(ServicePartner.ServiceCategories),
                   nameof(ServicePartner.ServiceCategories) + "." + nameof(ServiceCategory.Services)]).AsNoTracking();

            var foundServices = servicePartners
                 .SelectMany(sp => sp.ServiceCategories)
                 .SelectMany(sc => sc.Services)
                 .ToList()
                 .Where(service =>
                    services.Any(sc =>
                        sc.Name == service.Name &&
                        sc.Description == service.Description &&
                        sc.Price == service.Price &&
                        sc.Duration == service.Duration))
                .ToList();

            if (foundServices.Any())
            {
                return Errors.ServicePartner.DuplicateServices;
            }

            bool partnerExists = servicePartners
                .ToList()
                .Any(partner => partner.Name.
                    Equals(command.Name, StringComparison.CurrentCultureIgnoreCase));

            if (partnerExists)
            {
                return Errors.ServicePartner.DuplicateServicePartner;
            }

            var location = ServiceLocation.Create(
                command.Location.Latitude,
                command.Location.Longitude,
                command.Location.Address,
                command.Location.City,
                command.Location.Country,
                command.Location.PostalCode,
                command.Location.Description,
                command.Location.State);

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