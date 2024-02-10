using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAll
{
    public class ServicePartnerGetAllHandler(IUnitOfWork unitOfWork) 
        : IQueryHandler<ServicePartnerGetAllQuery, ServicePartnerGetAllResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<ServicePartnerGetAllResult>> Handle(
            ServicePartnerGetAllQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartners = await unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetAllAsync(cancellationToken);

            return new ServicePartnerGetAllResult(servicePartners.ToList());
        }
    }
}