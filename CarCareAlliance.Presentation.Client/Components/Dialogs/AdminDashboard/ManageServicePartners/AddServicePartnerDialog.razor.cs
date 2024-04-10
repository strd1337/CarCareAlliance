using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners
{
    public partial class AddServicePartnerDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public ServicePartner Model { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        private MudForm? form;
        private bool isValid;

        private void Cancel() => MudDialog.Cancel();

        private async Task Create()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            Model.Location = new ServiceLocation();
            Model.ServiceCategories = new List<ServiceCategory>();

            var isSuccess = await ServicePartnerService!.CreateAsync(Model);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
