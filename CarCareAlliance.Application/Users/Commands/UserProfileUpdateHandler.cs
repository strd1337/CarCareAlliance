using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Users.Common;
using CarCareAlliance.Domain.UserProfileAggregate;
using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;
using ErrorOr;
using CarCareAlliance.Domain.Common.Errors;

namespace CarCareAlliance.Application.Users.Commands
{
    public class UserProfileUpdateHandler(
        IUnitOfWork unitOfWork) :
        ICommandHandler<UserProfileUpdateCommand, UserProfileUpdateResult>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<ErrorOr<UserProfileUpdateResult>> Handle(
            UserProfileUpdateCommand command,
            CancellationToken cancellationToken)
        {
            var user = await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .GetByIdAsync(
                    UserProfileId.Create(command.UserId),
                    cancellationToken);

            if (user is null)
            {
                return Errors.UserProfile.NotFound;
            }

            var newUserInfo = UserDetailInformation.CreateNew(
                command.FirstName,
                command.LastName,
                command.PhoneNumber,
                command.Address,
                command.City,
                command.PostCode,
                command.Country);

            if (user.Information == newUserInfo)
            {
                return Errors.UserProfile.DataConflict;
            }

            user.UpdateInformation(newUserInfo);

            await unitOfWork
                .GetRepository<UserProfile, UserProfileId>()
                .UpdateAsync(user);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new UserProfileUpdateResult(
                user.Id.Value, user.Information);
        }
    }
}