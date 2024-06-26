﻿using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Components.Dialogs;
using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard;
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

        private async Task OnCreate()
        {
            var model = new ServicePartner();

            var parameters = new DialogParameters<AddServicePartnerDialog>
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

            var dialog = DialogService.Show<AddServicePartnerDialog>
                (string.Format("Add a new service partner", ["Service partner"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
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
            var parameters = new DialogParameters<EditWorkScheduleDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.WorkSchedules, servicePartner.WorkSchedules },
                { x => x.OwnerId, servicePartner.ServicePartnerId }
            };

            var options = new DialogOptions 
            { 
                CloseButton = true, 
                MaxWidth = MaxWidth.Medium, 
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<EditWorkScheduleDialog>
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
                { x => x.Model, servicePartner },
                { x => x.Refresh, () => table.ReloadServerData() }
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
        
        private async Task OnAddService(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<AddServiceDialog>
            {
                { x => x.Model, servicePartner },
                { x => x.Refresh, () => table.ReloadServerData() }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<AddServiceDialog>
                (string.Format("Add new services", ["Services"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }

        private async Task OnAddWorkSchedule(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<AddWorkScheduleDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.Owner, servicePartner.ServicePartnerId },
                { x => x.WorkSchedules, servicePartner.WorkSchedules },
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<AddWorkScheduleDialog>
                (string.Format("Add a new work schedule", ["Work schedule"]), parameters, options);

            var state = await dialog.Result;

            if (!state.Canceled)
            {
                await table.ReloadServerData();
            }
        }
    }
}
