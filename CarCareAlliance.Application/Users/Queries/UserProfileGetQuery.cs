using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Users.Common;

namespace CarCareAlliance.Application.Users.Queries
{
    public record UserProfileGetQuery(Guid UserProfileId)
        : IQuery<UserProfileGetResult>;
}