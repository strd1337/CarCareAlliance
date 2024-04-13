using CarCareAlliance.Presentation.Client.Models.Mechanics;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageMechanics
{
    public partial class AddMechanicDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public RegisterMechanicRequest Model { get; set; } = default!;
        [Parameter] public List<ServicePartner> ServicePartners { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IMechanicService? MechanicService { get; set; }

        private ServicePartner? selectedServicePartner;
        private MudForm? form;
        private bool isValid;
        private void Cancel() => MudDialog.Cancel();

        private async Task Save()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid && selectedServicePartner is null)
            {
                return;
            }

            Model.ServicePartnerId = selectedServicePartner!.ServicePartnerId;

            var isSuccess = await MechanicService!.RegisterAsync(Model);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
