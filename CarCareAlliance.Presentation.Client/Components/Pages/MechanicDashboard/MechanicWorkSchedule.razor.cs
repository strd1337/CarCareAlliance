using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.MechanicDashboard
{
    public partial class MechanicWorkSchedule
    {
        private string owner = string.Empty;

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            owner = await AuthenticationService!.GetMechanicIdAsync();

            await base.OnInitializedAsync();
        }

        private void OnAddWorkSchedule()
        {
            if (owner == string.Empty)
            {
                Snackbar.Add(Constants.DenyingNotification(), Severity.Error);
                return;
            }

            var parameters = new DialogParameters<AddWorkScheduleDialog>
            {
                { x => x.Owner, Guid.Parse(owner)  }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<AddWorkScheduleDialog>
                (string.Format("Add a new work schedule", ["Work schedule"]), parameters, options);
        }
    }
}
