using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.ServicePartners.Queries.Get
{
    public class ServicePartnerGetHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<ServicePartnerGetQuery, ServicePartnerGetResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerGetResult>> Handle(
            ServicePartnerGetQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartner = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetByIdAsync(
                    ServicePartnerId.Create(query.ServicePartnerId),
                    cancellationToken);

            if (servicePartner is null)
            {
                return Errors.ServicePartner.NotFound;
            }

            return new ServicePartnerGetResult(servicePartner);
        }
    }
}