using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageMechanics;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Mechanics;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.AdminDashboard
{
    public partial class ManageMechanics
    {
        private MudDataGrid<MechanicProfile> table = new();
        private MechanicProfile currentDto = new();
        private int defaultPageSize = 15;
        private string searchString = string.Empty;
        private PaginatedList<MechanicProfile>? list;

        [Inject]
        public IMechanicService? MechanicService { get; set; }

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        private async Task<GridData<MechanicProfile>> ServerReload(GridState<MechanicProfile> state)
        {
            var queryParams = new QueryParams
            {
                PageSize = state.PageSize,
                PageNumber = ++state.Page,
                SearchKey = searchString,
            };

            list = await MechanicService!.GetAllByFiltersAsync(queryParams);

            return new GridData<MechanicProfile> { TotalItems = list.TotalRecords, Items = list.Data };
        }

        private async Task OnSearch(string text)
        {
            searchString = text;
            await table.ReloadServerData();
        }

        private async Task OnRefresh()
        {
            searchString = string.Empty;
            await table.ReloadServerData();
        }

        private async Task OnCreate()
        {
            var response = await ServicePartnerService!.GetAllAsync();

            var model = new RegisterMechanicRequest();

            var parameters = new DialogParameters<AddMechanicDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.Model, model },
                { x => x.ServicePartners, response.ServicePartners }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<AddMechanicDialog>
                (string.Format("Register a new mechanic", ["Mechanic"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }
    }
}
