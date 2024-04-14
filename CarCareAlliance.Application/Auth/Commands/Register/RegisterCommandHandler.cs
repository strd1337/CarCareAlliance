using CarCareAlliance.Application.Auth.Common;
using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;

namespace CarCareAlliance.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IAuthRepository authRepository) :
        ICommandHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IAuthRepository authRepository = authRepository;

        public async Task<ErrorOr<AuthenticationResult>> Handle(
            RegisterCommand command, 
            CancellationToken cancellationToken)
        {
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
                UserDetailInformation.CreateNew(),
                RoleType.Driver);

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

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new AuthenticationResult(
                registeringUser.Id.Value,
                registeringUser,
                string.Empty);
        }
    }
}