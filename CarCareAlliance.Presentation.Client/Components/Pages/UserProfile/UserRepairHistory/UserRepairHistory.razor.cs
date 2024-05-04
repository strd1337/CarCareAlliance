using CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserRepairHistory;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Tickets;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.UserProfile.UserRepairHistory
{
    public partial class UserRepairHistory
    {
        private MudDataGrid<Ticket> table = new();
        private Ticket currentDto = new();
        private int defaultPageSize = 15;
        private PaginatedList<Ticket>? list;

        [Inject]
        public ITicketService? TicketService { get; set; }

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        private async Task<GridData<Ticket>> ServerReload(GridState<Ticket> state)
        {
            string? userId = await AuthenticationService!.GetUserIdAsync();

            if (userId is null)
            {
                return new GridData<Ticket> { TotalItems = 0, Items = [] };
            }

            var queryParams = new QueryParams
            {
                PageSize = state.PageSize,
                PageNumber = ++state.Page,
            };

            list = await TicketService!.GetAllByFiltersAndUserIdAsync(Guid.Parse(userId), queryParams);

            return new GridData<Ticket> { TotalItems = list.TotalRecords, Items = list.Data };
        }

        private async Task OnViewDetailsAsync(Ticket ticket)
        {
            var parameters = new DialogParameters<ViewTicketDetailsDialog>
            {
                { x => x.Ticket, ticket }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<ViewTicketDetailsDialog>
                (string.Format("View ticket detail information", ["Ticket"]), parameters, options);

            await dialog.Result;
        }
        
        private async Task OnCreateAsync()
        {
            var model = new Ticket();
            model.OrderDetails = new();

            var parameters = new DialogParameters<CreateTicketDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.Model, model }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<CreateTicketDialog>
                (string.Format("Create a new ticket", ["Ticket"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }

        private async Task OnRefresh()
        {
            await table.ReloadServerData();
        }
    }
}
