namespace CarCareAlliance.Contracts.Staff.Register
{
    public record StaffRegisterResponse(
        Guid AuthenticationId,
        Guid UserProfileId,
        Guid MechanicId,
        Guid ServicePartnerId);
}