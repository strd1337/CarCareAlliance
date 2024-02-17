namespace CarCareAlliance.Contracts.Staff.Register
{
    public record StaffRegisterRequest(
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
        Guid ServicePartnerId);
}