using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace CarCareAlliance.Presentation.Client.Components
{
    public partial class NavBar : ComponentBase
    {
        private bool isNavBarOpen = false;

        [Inject]
        private IAuthenticationService? AuthenticationService { get; set; }

        public async Task LogOut()
        {
            await AuthenticationService!.LogOutAsync();
        }

        void ToggleDrawer()
        {
            isNavBarOpen = !isNavBarOpen;
        }
    }
}