namespace CarCareAlliance.Presentation.Client.Models.Auth
{
    public record RegisterRequest(
        string Username,
        string Email,
        string Password);
}
