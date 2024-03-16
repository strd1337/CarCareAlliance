using CarCareAlliance.Presentation.Client.Models.Auth;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components.Pages
{
    public partial class Login
    {
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isValid;
        
        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        public async Task LogIn()
        {
            var loginRequest = new LoginRequest(email, password);

            await AuthenticationService!.LogInAsync(loginRequest);

            if (await AuthenticationService.IsUserAuthenticated())
            {
                NavigationManager?.NavigateTo("/home");
            }
        }
    }

}
