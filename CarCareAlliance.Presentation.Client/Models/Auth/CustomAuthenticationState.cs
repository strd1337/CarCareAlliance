using Blazored.LocalStorage;
using CarCareAlliance.Presentation.Client.Common.Auth;
using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CarCareAlliance.Presentation.Client.Models.Auth
{
    public class CustomAuthenticationState(ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await GetTokenAsync();
                return token is null
                    ? await Task.FromResult(new AuthenticationState(_anonymous))
                    : await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtTokenParser.ParseClaimsFromJwt(token), "JwtAuth"))));
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(string? token)
        {
            var authenticatedUser = _anonymous;
            if (string.IsNullOrEmpty(token))
            {
                await _localStorage.RemoveItemAsync(Constants.LocalStorage.JwtTokenKey);
            }
            else
            {
                authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtTokenParser.ParseClaimsFromJwt(token), "JwtAuth"));
                await _localStorage.SaveItemEncryptedAsync(Constants.LocalStorage.JwtTokenKey, token);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
        }

        public async Task<string?> GetTokenAsync()
        {
            string token = default!;
            var userToken = await _localStorage.ReadEncryptedItemAsync<string>(Constants.LocalStorage.JwtTokenKey);
            if (userToken is not null && DateTime.UtcNow < new JwtSecurityToken(userToken).ValidTo)
            {
                token = userToken;
            }
            return token;
        }
    }
}