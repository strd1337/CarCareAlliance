using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAllCategories
{
    public class GetAllServicePartnersCategoriesHandler(IUnitOfWork unitOfWork) : 
        IQueryHandler<GetAllServicePartnersCategoriesQuery, GetAllServicePartnersCategoriesResult>
    {
        public async Task<ErrorOr<GetAllServicePartnersCategoriesResult>> Handle(
            GetAllServicePartnersCategoriesQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartners = unitOfWork
               .GetRepository<ServicePartner, ServicePartnerId>()
               .GetAll([nameof(ServicePartner.ServiceCategories),
                   nameof(ServicePartner.ServiceCategories) + "." + nameof(ServiceCategory.Services)]);

            var serviceCategories = servicePartners.SelectMany(sp => sp.ServiceCategories).AsNoTracking();

            return new GetAllServicePartnersCategoriesResult(serviceCategories.ToList());
        }
    }
}
