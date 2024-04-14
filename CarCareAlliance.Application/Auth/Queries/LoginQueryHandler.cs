using CarCareAlliance.Application.Auth.Common;
using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using CarCareAlliance.Domain.MechanicAggregate;
using CarCareAlliance.Domain.MechanicAggregate.ValueObjects;

namespace CarCareAlliance.Application.Auth.Queries
{
    public class LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork) :
        IQueryHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator = jwtTokenGenerator;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<AuthenticationResult>> Handle(
            LoginQuery query,
            CancellationToken cancellationToken)
        {
            var authUser = await unitOfWork
                .GetRepository<Authentication, AuthenticationId>()
                .FirstOrDefaultAsync(
                    x => x.Email == query.Email,
                    cancellationToken);

            if (authUser is null ||
                !BCrypt.Net.BCrypt.Verify(query.Password, authUser.PasswordHash))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var user = await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(
                    authUser.UserProfileId,
                    cancellationToken);

            var mechanic = await unitOfWork
                .GetRepository<MechanicProfile, MechanicProfileId>()
                .FirstOrDefaultAsync(
                    x => x.UserProfileId == UserProfileId.Create(user!.Id.Value),
                    cancellationToken);

            var token = jwtTokenGenerator.GenerateToken(authUser, user!, mechanic ?? null);

            return new AuthenticationResult(
                authUser.Id.Value,
                authUser,
                token);
        }
    }
}