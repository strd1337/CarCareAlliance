namespace CarCareAlliance.Contracts.Authentication
{
    public record AuthenticationResponse(
        Guid AuthenticationId,
        Guid UserProfileId,
        string UserName,
        string Email,
        string Token);
}
