using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard;
using CarCareAlliance.Presentation.Client.Components.Dialogs.AdminDashboard.ManageServicePartners;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.Main
{
	public partial class ServicePartnersSlider
	{
        private MudCarousel<ServicePartner> carousel = new();
        private PaginatedList<ServicePartner> list = new();
        private List<ServicePartner> source = [];

        private int selectedIndex = 0;
        private int defaultSize = 10;
        private int currentMaxSize = 10;
        private int defaultNumber = 1;
        private int totalItems = 0;

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetData();

            totalItems = list!.TotalRecords;

            await base.OnInitializedAsync();
        }

        private async Task GetData()
        {
            var queryParams = new QueryParams
            {
                PageSize = defaultSize,
                PageNumber = defaultNumber,
            };

            list = await ServicePartnerService!.GetAllByFiltersAsync(queryParams);

            source = list.Data;
        }

        private async Task NextStepAsync()
        {
            currentMaxSize += defaultSize;
            selectedIndex = 0;
            defaultNumber++;
            await GetData();
            StateHasChanged();
        }

        private async Task PreviousStepAsync()
        {
            currentMaxSize -= defaultSize;
            selectedIndex = 0;
            defaultNumber--;
            await GetData();
            StateHasChanged();
        }

        private void OnViewLocation(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditLocationDialog>
            {
                { x => x.Model, servicePartner },
                { x => x.IsReadOnly, true }
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
        }

        private void OnViewMechanics(ServicePartner servicePartner)
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
        }

        private void OnViewServiceCategories(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditServiceCategoriesDialog>
            {
                { x => x.Model, servicePartner },
                { x => x.IsReadOnly, true }
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
        }

        private void OnViewWorkSchedules(ServicePartner servicePartner)
        {
            var parameters = new DialogParameters<EditWorkScheduleDialog>
            {
                { x => x.WorkSchedules, servicePartner.WorkSchedules },
                { x => x.OwnerId, servicePartner.ServicePartnerId },
                { x => x.IsReadOnly, true }
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
        }
    }
}
