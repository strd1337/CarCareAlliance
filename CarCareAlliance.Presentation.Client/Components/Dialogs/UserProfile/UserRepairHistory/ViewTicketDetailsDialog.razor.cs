using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Models.Tickets;
using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserRepairHistory
{
    public partial class ViewTicketDetailsDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Ticket Ticket { get; set; } = new();

        [Inject]
        public IVehicleService? VehicleService { get; set; }

        private MudForm? form;
        private bool isValid;
        private Service? selectedService = default!;
        private Vehicle Vehicle { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var result = await VehicleService!.GetByVinAsync(Ticket.VehicleVin);

            if (result is null)
            {
                Snackbar.Add("The vehicle was not found!", Severity.Error);
            }
            else
            {
                Vehicle = result;
            }
            
            await base.OnInitializedAsync();
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
