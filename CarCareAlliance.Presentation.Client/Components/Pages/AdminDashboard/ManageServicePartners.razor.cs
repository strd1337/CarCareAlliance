using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;
using static MudBlazor.CategoryTypes;

namespace CarCareAlliance.Presentation.Client.Components.Pages.AdminDashboard
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

        private async Task OnViewLocation(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<ViewLocationDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.Model, servicePartner }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<ViewLocationDialog>
                (string.Format("Location detail information", ["Location detail information"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }

        private async Task OnViewMechanics(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<ViewMechanicsDialog>
            {
                { x => x.Model, servicePartner }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<ViewMechanicsDialog>
                (string.Format("Mechanic profiles", ["Mechanic profiles"]), parameters, options);

            await dialog.Result;
        }

        private async Task OnViewServiceCategories(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<ViewServiceCategoriesDialog>
            {
                { x => x.Model, servicePartner }
            };

            var options = new DialogOptions 
            { 
                CloseButton = true, 
                MaxWidth = MaxWidth.Medium, 
                FullWidth = true, 
                CloseOnEscapeKey = true 
            };

            var dialog = DialogService.Show<ViewServiceCategoriesDialog>
                (string.Format("Service categories", ["Service categories"]), parameters, options);

            await dialog.Result;
        }
        
        // TODO:
        private async Task OnAddService(ServicePartner servicePartner)
        {
        }
    }
}
