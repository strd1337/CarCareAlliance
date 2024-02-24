using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using ErrorOr;

namespace CarCareAlliance.Application.Vehicles.Queries.GetByUserId
{
    public class VehicleGetByUserIdHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<VehicleGetByUserIdQuery, VehicleGetByUserIdResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<VehicleGetByUserIdResult>> Handle(
            VehicleGetByUserIdQuery query,
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

            var vehicles = await unitOfWork
                .GetRepository<Vehicle, VehicleId>()
                .GetWhereAsync(
                    x => x.UserProfileId == UserProfileId.Create(query.UserId), 
                    cancellationToken);

            return new VehicleGetByUserIdResult(vehicles);
        }
    }
}
