using CarCareAlliance.Application.Common.CQRS;
using CarCareAlliance.Application.Staff.Common;

namespace CarCareAlliance.Application.Staff.Commands
{
    public record StaffRegisterCommand(
        string UserName,
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        string Country,
        string City,
        string PostCode,
        float Experience,
        Guid ServicePartnerId) : ICommand<StaffRegisterResult>;
}