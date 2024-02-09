using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.ServicePartners.Commands.Delete
{
    public class ServicePartnerDeleteHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<ServicePartnerDeleteCommand, ServicePartnerDeleteResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerDeleteResult>> Handle(
            ServicePartnerDeleteCommand command,
            CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetByIdAsync(
                    ServicePartnerId.Create(command.ServicePartnerId),
                    cancellationToken);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

            await unitOfWork
               .GetRepository<ServicePartner, ServicePartnerId>()
               .RemoveAsync(servicePartner);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServicePartnerDeleteResult(
                servicePartner.Id.Value, 
                servicePartner.Name);
        }
    }
}