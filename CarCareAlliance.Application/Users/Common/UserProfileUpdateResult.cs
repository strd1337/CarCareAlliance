using CarCareAlliance.Domain.UserProfileAggregate.ValueObjects;

namespace CarCareAlliance.Application.Users.Common
{
    public record UserProfileUpdateResult(
        Guid UserId,
        UserDetailInformation Information);
}
