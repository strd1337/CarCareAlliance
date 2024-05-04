using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserVehicles
{
    public partial class ViewVehicleDetailsDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Vehicle Vehicle { get; set; } = new();

        [Inject]
        public IVehicleService? VehicleService { get; set; }

        private MudForm? form;
        private bool isValid;

        protected override async Task OnInitializedAsync()
        {
            var result = await VehicleService!.GetByVinAsync(Vehicle.Vin);

            if (result is null)
            {
                Snackbar.Add("The vehicle was not found!", Severity.Error);
                MudDialog.Cancel();
            }
            else
            {
                result.VehicleId = Vehicle.VehicleId;
                result.UserProfileId = Vehicle.UserProfileId;
                result.LicensePlate = Vehicle.LicensePlate;
                Vehicle = result;
                isValid = true;
            }

            await base.OnInitializedAsync();
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
