namespace CarCareAlliance.Contracts.Users.UpdateProfile
{
    public record UserProfileUpdateRequest(
        Guid UserId,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        string City,
        string PostCode,
        string Country);
}
