using CarCareAlliance.Presentation.Client.Models.Auth;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace CarCareAlliance.Presentation.Client.Components.Pages
{
    public partial class Login
    {
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isValid;
        private string username = string.Empty;
        private int activeIndex = 0;

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

        public async Task Register()
        {
            var registerRequest = new RegisterRequest(username, email, password);

            await AuthenticationService!.RegisterAsync(registerRequest);

            activeIndex = 0;
            email = string.Empty;
            password = string.Empty;
            isValid = false;
            username = string.Empty;
        }
    }
}
