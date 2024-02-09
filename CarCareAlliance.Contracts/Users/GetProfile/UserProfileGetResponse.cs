namespace CarCareAlliance.Contracts.Users.GetProfile
{
    public record UserProfileGetResponse(
        Guid UserId,
        string UserName,
        string Email,
        string FirstName,
        string LastName,
        string PhoneNumber,
        string Address,
        string City,
        string PostCode,
        string Country);
}
