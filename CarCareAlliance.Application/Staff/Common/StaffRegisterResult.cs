namespace CarCareAlliance.Application.Staff.Common
{
    public record StaffRegisterResult(
        Guid AuthenticationId,
        Guid UserProfileId,
        Guid MechanicId,
        Guid ServicePartnerId);
}