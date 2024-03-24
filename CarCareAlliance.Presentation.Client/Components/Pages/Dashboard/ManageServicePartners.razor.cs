using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Dashboard
{
    public partial class ManageServicePartners
    {
        private MudDataGrid<ServicePartner> table = new();
        private ServicePartner currentDto = new();
        private int defaultPageSize = 15;
        private string searchString = string.Empty;
        private PaginatedList<ServicePartner>? list;

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        private async Task<GridData<ServicePartner>> ServerReload(GridState<ServicePartner> state)
        {
            var queryParams = new QueryParams
            {
                PageSize = state.PageSize,
                PageNumber = ++state.Page,
                SearchKey = searchString,
            };

            list = await ServicePartnerService!.GetAllByFiltersAsync(queryParams);

            return new GridData<ServicePartner> { TotalItems = list.TotalRecords, Items = list.Data };
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

        // TODO: 
        private async Task OnCreate()
        {
        }

        // TODO: 
        private async Task OnDelete(ServicePartner servicePartner)
        {  
        }

        // TODO: 
        private async Task OnEdit(ServicePartner servicePartner)
        { 
        }
    }
}
