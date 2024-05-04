using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserVehicles
{
    public partial class AddVehicleDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Vehicle Vehicle { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }
        [Parameter] public Guid UserProfileId { get; set; }

        [Inject]
        public IVehicleService? VehicleService { get; set; }

        private MudForm? form;
        private bool isValid;
        private bool infoIsGot = false;

        private void Cancel() => MudDialog.Cancel();

        private async Task AddAsync()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            Vehicle.UserProfileId = UserProfileId;

            var isSuccess = await VehicleService!.AddAsync(Vehicle);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private async Task OnGetInfoAsync()
        {
            var result = await VehicleService!.GetByVinAsync(Vehicle.Vin);

            if (result is null)
            {
                Snackbar.Add("The vehicle was not found!", Severity.Error);
            }
            else
            {
                Vehicle = result;
                infoIsGot = true;
            }
        }

        private void RefreshInfo()
        {
            infoIsGot = false;
            Vehicle = new();
        }
    }
}
