using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.VehicleAggregate;


namespace CarCareAlliance.Application.Vehicles.Queries
{
    public class VehicleGetHandler(IUnitOfWork unitOfWork)
        : IQueryHandler<VehicleGetQuery, VehicleGetResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<VehicleGetResult>> Handle(
            VehicleGetQuery query,
            CancellationToken cancellationToken)
        {
            var vehicle = await unitOfWork
                .GetRepository<Vehicle, VehicleId>()
                .GetByIdAsync(
                    VehicleId.Create(query.VehicleId),
                    cancellationToken);

            if (vehicle is null)
            {
                return Errors.Vehicle.NotFound;
            }

            return new VehicleGetResult(vehicle);
        }
    }
}
