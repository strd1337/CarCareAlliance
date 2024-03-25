namespace CarCareAlliance.Contracts.Staff.Common
{
    public record MechanicDto(
        Guid MechanicId,
        Guid ProfileId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Country,
        string City,
        float Experience);
}
