﻿using CarCareAlliance.Presentation.Client.Models.Auth;

namespace CarCareAlliance.Presentation.Client.Services.Interfaces
{
    public interface IAuthenticationService
    {

        Task LogInAsync(LoginRequest loginRequest);
        Task LogOutAsync();
        Task<bool> IsUserAuthenticated();
        Task<string?> GetJwtTokenAsync();
        Task<string?> GetUserIdAsync();
    }
}