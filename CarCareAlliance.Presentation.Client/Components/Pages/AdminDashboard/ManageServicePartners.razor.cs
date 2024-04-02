using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Components.Dialogs;
using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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

        private async Task OnDelete(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<DeleteConfirmation>
            {
                { x => x.ContentText, Constants.DeleteConfirmantion(servicePartner.Name) }
            };

            var options = new DialogOptions 
            { 
                CloseButton = true, 
                MaxWidth = MaxWidth.Medium, 
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<DeleteConfirmation>(
                string.Format("Delete the service partner", ["Service partner"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await ServicePartnerService!.DeleteAsync(servicePartner);
                await table.ReloadServerData();
            }
        }

        private async Task OnEditWorkSchedules(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditServicePartnerWorkScheduleDialog>
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

            var dialog = DialogService.Show<EditServicePartnerWorkScheduleDialog>
                (string.Format("Work schedule detail information", ["Work schedule"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }

        private async Task OnEditLocation(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditLocationDialog>
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

            var dialog = DialogService.Show<EditLocationDialog>
                (string.Format("Location detail information", ["Location"]), parameters, options);

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

        private async Task OnEditServiceCategories(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditServiceCategoriesDialog>
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

            var dialog = DialogService.Show<EditServiceCategoriesDialog>
                (string.Format("Service categories", ["Service categories"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }
        
        // TODO:
        private async Task OnAddService(ServicePartner servicePartner)
        {
        }

        // TODO:
        private async Task OnAddWorkSchedule(ServicePartner servicePartner)
        {
        }
    }
}
