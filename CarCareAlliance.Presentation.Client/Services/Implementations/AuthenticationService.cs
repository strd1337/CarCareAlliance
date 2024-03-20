using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Models.Auth;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;

namespace CarCareAlliance.Presentation.Client.Services.Implementations
{
    public class AuthenticationService(
        IHttpClientFactory httpClientFactory, 
        ISnackbar snackbar, 
        HttpErrorsService httpErrorsService, 
        AuthenticationStateProvider authenticationState) : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ISnackbar _snackbar = snackbar;
        private readonly HttpErrorsService _httpErrorsService = httpErrorsService;
        private readonly AuthenticationStateProvider _state = authenticationState;

        public async Task LogInAsync(LoginRequest loginRequest)
        {
            var response = await _httpClientFactory.CreateClient(Constants.Client).PostAsync("auth/login", JsonContent.Create(loginRequest));
            await _httpErrorsService.EnsureSuccessStatusCode(response);
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();

            await ((CustomAuthenticationState)_state).UpdateAuthenticationState(result.Token);
        }

        public async Task RegisterAsync(RegisterRequest registerRequest)
        {
            var response = await _httpClientFactory.CreateClient(Constants.Client).PostAsync("auth/register", JsonContent.Create(registerRequest));
            await _httpErrorsService.EnsureSuccessStatusCode(response);

            _snackbar.Add("Successfully registered!", Severity.Success);
        }
        public async Task LogOutAsync()
        {
            await ((CustomAuthenticationState)_state).UpdateAuthenticationState(string.Empty);
            _snackbar.Add("Logout successfully", Severity.Info);
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var token = await ((CustomAuthenticationState)_state).GetTokenAsync();
            return !string.IsNullOrWhiteSpace(token);
        }

        public async Task<string?> GetJwtTokenAsync()
        {
            return await ((CustomAuthenticationState)_state).GetTokenAsync();
        }

        public async Task<string?> GetUserIdAsync()
        {
            var userId = (await _state.GetAuthenticationStateAsync())
                .User.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
            return userId;
        }
    }
}
