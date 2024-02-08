using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Users.Common;
using CarCareAlliance.Domain.AuthenticationAggregate;
using CarCareAlliance.Domain.AuthenticationAggregate.ValueObjects;
using CarCareAlliance.Domain.Common.Errors;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Application.Users.Queries
{
    public class UserProfileHandler(
        IUnitOfWork unitOfWork) :
       IQueryHandler<UserProfileGetQuery, UserProfileGetResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr.ErrorOr<UserProfileGetResult>> Handle(
            UserProfileGetQuery query, 
            CancellationToken cancellationToken)
        {
            var userProfile = await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(
                    UserProfileId.Create(query.UserProfileId),
                    cancellationToken);

            if (userProfile is null)
            {
                return Errors.UserProfile.NotFound;
            }

            var authUser = await unitOfWork
                .GetRepository<Authentication, AuthenticationId>()
                .FirstOrDefaultAsync(
                    x => x.UserProfileId == userProfile.Id,
                    cancellationToken);

            if (authUser is null)
            {
                return Errors.UserProfile.NotFound;
            }

            return new UserProfileGetResult(authUser, userProfile);
        }
    }
}
