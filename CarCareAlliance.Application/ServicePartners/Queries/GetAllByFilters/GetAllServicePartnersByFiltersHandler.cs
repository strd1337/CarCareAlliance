using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.ServicePartners.Common;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.Entities;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.WorkScheduleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.WorkScheduleAggregate;
using CarCareAlliance.Domain.UserProfileAggregate;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Application.ServicePartners.Queries.GetAllByFilters
{
    public class GetAllServicePartnersByFiltersHandler(
        IUnitOfWork unitOfWork) : IQueryHandler<GetAllServicePartnersByFiltersQuery, PagedResult<ServicePartnerResult>>
    {
        public async Task<ErrorOr<PagedResult<ServicePartnerResult>>> Handle(
            GetAllServicePartnersByFiltersQuery query,
            CancellationToken cancellationToken)
        {
            var servicePartners = unitOfWork
                .GetRepository<ServicePartner, ServicePartnerId>()
                .GetAll([nameof(ServicePartner.ServiceCategories),
                    nameof(ServicePartner.ServiceCategories) + "." + nameof(ServiceCategory.Services)]);

            var searchKeys = query.SearchKey.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var key in searchKeys)
            {
                servicePartners = servicePartners.Where(sp =>
                    EF.Functions.Like(sp.Name.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.Description.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.ServiceLocation.Address.ToLower(), $"%{key}%"));
            }

            int resultsCount = servicePartners.Count();
            
            var servicePartnersResult = servicePartners.ToList().OrderBy(x => x.ModifiedDate)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();

            var result = new List<ServicePartnerResult>();
            foreach (var servicePartner in servicePartnersResult)
            {
                var mechanicProfileIds = servicePartner.MechanicProfileIds
                    .Select(id => MechanicProfileId.Create(id.Value))
                    .ToList();

                var mechanics = unitOfWork
                    .GetRepository<MechanicProfile, MechanicProfileId>()
                    .GetWhere(x => mechanicProfileIds.Contains(x.Id)).ToList();

                var workSchedules = unitOfWork
                    .GetRepository<WorkSchedule, WorkScheduleId>()
                    .GetWhere(x => x.OwnerId == servicePartner.Id.Value);

                var userProfileIds = mechanics
                    .Select(mechanic => MechanicProfileId.Create(mechanic.UserProfileId.Value))
                    .ToList();

                var mechanicProfiles = unitOfWork
                    .GetRepository<UserProfile, UserProfileId>()
                    .GetWhere(x => userProfileIds.Contains(x.Id));

                result.Add(new ServicePartnerResult
                {
                    ServicePartner = servicePartner,
                    Mechanics = mechanics,
                    WorkSchedules = workSchedules.ToList(),
                    MechanicProfiles = mechanicProfiles.ToList()
                });
            }

            var pagedResult = new PagedResult<ServicePartnerResult>(
                query.PageNumber,
                query.PageSize,
                resultsCount,
                result);

            return pagedResult;
        }
    }
}
