using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Staff.Common;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Application.Staff.Queries
{
    public class GetAllStaffsByFiltersHandler(
        IUnitOfWork unitOfWork) : IQueryHandler<GetAllStaffsByFiltersQuery, PagedResult<GetAllStaffsByFiltersResult>>
    {
        public async Task<ErrorOr<PagedResult<GetAllStaffsByFiltersResult>>> Handle(
            GetAllStaffsByFiltersQuery query, CancellationToken cancellationToken)
        {
            var userProfiles = unitOfWork.GetRepository<UserProfile, UserProfileId>()
                .GetAll();

            var searchKeys = query.SearchKey.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var key in searchKeys)
            {
                userProfiles = userProfiles.Where(sp =>
                    EF.Functions.Like(sp.Information.FirstName!.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.Information.LastName!.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.Information.PhoneNumber!.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.Information.Country!.ToLower(), $"%{key}%") ||
                    EF.Functions.Like(sp.Information.City!.ToLower(), $"%{key}%"));
            }

            var mechanics = unitOfWork.GetRepository<MechanicProfile, MechanicProfileId>()
                .GetAll()
                .ToList();

            var mechanicUserProfileIds = mechanics
                .Select(mechanic => mechanic.UserProfileId)
                .ToList();

            var mechanicProfiles = userProfiles
                .ToList()
                .Where(profile => mechanicUserProfileIds
                    .Contains(UserProfileId.Create(profile.Id.Value)));

            var mechanicProfilesResult = mechanicProfiles
                .ToList()
                .OrderBy(x => x.ModifiedDate)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            var result = new List<GetAllStaffsByFiltersResult>();
            foreach (var profile in mechanicProfilesResult)
            {
                var mechanic = mechanics
                    .FirstOrDefault(mechanic => UserProfileId.Create(mechanic.UserProfileId.Value) == UserProfileId.Create(profile.Id.Value));

                if (mechanic is not null)
                {
                    result.Add(new GetAllStaffsByFiltersResult
                    {
                        Mechanic = mechanic,
                        MechanicProfile = profile
                    });
                }
            }

            int resultsCount = result.Count();

            var pagedResult = new PagedResult<GetAllStaffsByFiltersResult>(
                query.PageNumber,
                query.PageSize,
                resultsCount,
                result);

            return pagedResult;
        }
    }
}
