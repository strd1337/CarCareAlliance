using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners
{
    public partial class ViewLocationDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public ServicePartner Model { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        private MudForm? form;

        private void Cancel() => MudDialog.Cancel();

        private async Task Save()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            //TO DO: 
            //var isSuccess = await ServicePartnerService!.UpdateAsync(Model);
            if (true)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
