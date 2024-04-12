using CarCareAlliance.Presentation.Client.Models.Auth;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Auth
{
    public partial class Login
    {
        private string loginEmail = string.Empty;
        private string loginPassword = string.Empty;

        private string registerEmail = string.Empty;
        private string registerPassword = string.Empty;
        private string registerUsername = string.Empty;

        private int activeIndex = 0;
        private bool isValid;

        [Inject]
        public NavigationManager? NavigationManager { get; set; }
        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        public async Task LogIn()
        {
            var loginRequest = new LoginRequest(loginEmail, loginPassword);

            await AuthenticationService!.LogInAsync(loginRequest);

            if (await AuthenticationService.IsUserAuthenticated())
            {
                NavigationManager?.NavigateTo("/home");
            }
        }

        public async Task Register()
        {
            var registerRequest = new RegisterRequest(registerUsername, registerEmail, registerPassword);

            await AuthenticationService!.RegisterAsync(registerRequest);

            activeIndex = 0;
            registerEmail = string.Empty;
            registerPassword = string.Empty;
            isValid = false;
            registerUsername = string.Empty;
        }
    }
}
