namespace CarCareAlliance.Presentation.Client.Models.Auth
{
    public record AuthenticationResponse(
         Guid AuthenticationId,
         Guid UserProfileId,
         string UserName,
         string Email,
         string Token);
}
