using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Vehicles.Common;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.VehicleAggregate;
using CarCareAlliance.Domain.VehicleAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;

namespace CarCareAlliance.Application.Vehicles.Commands.Add
{
    public class VehicleAddHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<VehicleAddCommand, VehicleAddResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<VehicleAddResult>> Handle(
            VehicleAddCommand command,
            CancellationToken cancellationToken)
        {
            if (unitOfWork
                .GetRepository<Vehicle, VehicleId>()
                .FirstOrDefaultAsync(
                    x => x.Vin.Equals(command.Vin),
                    cancellationToken).Result is not null)
            {
                return Errors.Vehicle.DuplicateVehicle;
            }

            if (await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(
                    UserProfileId.Create(command.UserProfileId),
                    cancellationToken) is null)
            {
                return Errors.UserProfile.NotFound;
            }

            var vehicle = Vehicle.Create(
                command.Brand,
                command.Model,
                command.Year,
                command.Vin,
                command.LicensePlate,
                UserProfileId.Create(command.UserProfileId));

            await unitOfWork
                .GetRepository<Vehicle, VehicleId>()
                .AddAsync(vehicle, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new VehicleAddResult(vehicle);
        }
    }
}