using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Staff.Common;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.ServicePartnerAggregate.ValueObjects;
using CarCareAlliance.Domain.ServicePartnerAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;

namespace CarCareAlliance.Application.Staff.Commands
{
    public class StaffRegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthRepository authRepository) :
        ICommandHandler<StaffRegisterCommand, StaffRegisterResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IAuthRepository authRepository = authRepository;

        public async Task<ErrorOr<StaffRegisterResult>> Handle(
            StaffRegisterCommand command,
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

            if (await authRepository.IsEmailNotUniqueAsync(
                command.Email, cancellationToken))
            {
                return Errors.UserProfile.DuplicateEmail;
            }

            if (await authRepository.IsUsernameNotUniqueAsync(
                command.UserName, cancellationToken))
            {
                return Errors.UserProfile.DuplicateUsername;
            }

            var user = UserProfile.Create(
                UserDetailInformation.CreateNew(
                    command.FirstName,
                    command.LastName,
                    command.PhoneNumber,
                    command.Address,
                    command.City,
                    command.PostCode,
                    command.Country),
                RoleType.Mechanic);

            await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .AddAsync(user, cancellationToken);

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt
                .HashPassword(command.Password, salt);

            var registeringUser = Authentication.Create(
               command.UserName,
               command.Email,
               passwordHash,
               salt,
               UserProfileId.Create(user.Id.Value));

            await unitOfWork
                .GetRepository<Authentication, AuthenticationId>()
                .AddAsync(registeringUser, cancellationToken);

            var mechanic = MechanicProfile.Create(
                command.Experience,
                UserProfileId.Create(user.Id.Value),
                ServicePartnerId.Create(servicePartner.Id.Value));

            await unitOfWork
                .GetRepository<MechanicProfile, MechanicProfileId>()
                .AddAsync(mechanic, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new StaffRegisterResult(
                registeringUser.Id.Value,
                user.Id.Value,
                mechanic.Id.Value,
                servicePartner.Id.Value);
        }
    }
}
