using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components
{
    public partial class NavBar : ComponentBase
    {
        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        public async Task LogOut()
        {
            await AuthenticationService!.LogOutAsync();
        }
    }
}
