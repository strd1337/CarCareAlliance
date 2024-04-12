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

        }
        
        private async Task OnEditProfile(MechanicProfile mechanicProfile)
        {

        }
    }
}
