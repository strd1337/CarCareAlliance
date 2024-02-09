using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Users.Common;

namespace CarCareAlliance.Application.Users.Commands
{
    public record UserProfileUpdateCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        string City,
        string PostCode,
        string Country) : ICommand<UserProfileUpdateResult>;
}
