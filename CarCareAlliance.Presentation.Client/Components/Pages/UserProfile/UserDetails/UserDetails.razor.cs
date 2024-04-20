using CarCareAlliance.Presentation.Client.Models.Users;
using CarCareAlliance.Presentation.Client.Services.Implementations;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.UserProfile.UserDetails
{
    public partial class UserDetails
    {
        public User User { get; set; } = new();

        [Inject]
        public IUserService? UserService { get; set; }

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        private MudForm? form;
        private bool isValid;

        protected override async Task OnInitializedAsync()
        {
            string? userId = await AuthenticationService!.GetUserIdAsync();

            if (userId is not null)
            {
                User = await UserService!.GetAsync(userId);
            }

            await base.OnInitializedAsync();
        }

        public async Task SaveAsync()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            await UserService!.UpdateAsync(User);
        }
    }
}
