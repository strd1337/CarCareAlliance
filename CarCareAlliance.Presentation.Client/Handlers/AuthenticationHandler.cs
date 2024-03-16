using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;

namespace CarCareAlliance.Presentation.Client.Handlers
{
    public class AuthenticationHandler(IAuthenticationService authenticationService, NavigationManager navigationManager) : DelegatingHandler
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly NavigationManager _navigationManager = navigationManager;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _authenticationService.GetJwtTokenAsync();
            var isToServer = request.RequestUri?.AbsoluteUri.StartsWith(_navigationManager.BaseUri) ?? false;

            if (isToServer && !string.IsNullOrEmpty(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
