using CarCareAlliance.Presentation.Client.Models.Tickets;
using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.MechanicDashboard
{
    public partial class UpdateTicketDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Ticket Ticket { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }
        [Parameter] public bool UpdateStatus { get; set; }

        [Inject]
        public ITicketService? TicketService { get; set; }

        private MudForm? form;
        private bool isValid;
        private RepairStatus selectedRepairStatus;

        private void Cancel() => MudDialog.Cancel();

        private async Task Save()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            var request = new UpdateTicketRequest
            {
                TicketId = Ticket.TicketId,
                Comments = Ticket.OrderDetails.Comments,
                RepairStatus = selectedRepairStatus
            };

            var isSuccess = await TicketService!.UpdateAsync(request);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
