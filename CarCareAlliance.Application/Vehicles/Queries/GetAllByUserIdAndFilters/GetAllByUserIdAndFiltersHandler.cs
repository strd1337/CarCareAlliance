using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Pagination;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.VehicleAggregate;

namespace CarCareAlliance.Application.Vehicles.Queries.GetAllByUserIdAndFilters
{
    public class GetAllByUserIdAndFiltersHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<GetAllByUserIdAndFiltersQuery, PagedResult<VehicleGetResult>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<PagedResult<VehicleGetResult>>> Handle(
            GetAllByUserIdAndFiltersQuery query,
            CancellationToken cancellationToken)
        {
            var user = await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(
                    UserProfileId.Create(query.UserId),
                    cancellationToken);

            if (user is null)
            {
                return Errors.UserProfile.NotFound;
            }

            var vehicles = unitOfWork
                .GetRepository<Vehicle, VehicleId>()
                .GetWhere(x => x.UserProfileId == UserProfileId.Create(query.UserId));

            int resultsCount = vehicles.Count();
            
            var result = vehicles.ToList().OrderBy(x => x.ModifiedDate)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList()
                .Select(x => new VehicleGetResult(x))
                .ToList();

            var pagedResult = new PagedResult<VehicleGetResult>(
                query.PageNumber,
                query.PageSize,
                resultsCount,
                result);

            return pagedResult;
        }
    }
}
