using CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserVehicles;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CarCareAlliance.Presentation.Client.Components.Pages.UserProfile.UserVehicles
{
    public partial class UserVehicles
    {
        private MudDataGrid<Vehicle> table = new();
        private Vehicle currentDto = new();
        private int defaultPageSize = 15;
        private PaginatedList<Vehicle>? list;

        [Inject]
        public IVehicleService? VehicleService { get; set; }

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }


        private async Task<GridData<Vehicle>> ServerReload(GridState<Vehicle> state)
        {
            var userId = await AuthenticationService!.GetUserIdAsync();

            if (userId is null)
            {
                list = new();
            }
            else
            {
                var queryParams = new QueryParams
                {
                    PageSize = state.PageSize,
                    PageNumber = ++state.Page,
                };

                list = await VehicleService!.GetAllByUserIdAsync(userId, queryParams);
            }

            return new GridData<Vehicle> { TotalItems = list.TotalRecords, Items = list.Data };
        }

        private async Task OnViewDetailsAsync(Vehicle vehicle)
        {
            var parameters = new DialogParameters<ViewVehicleDetailsDialog>
            {
                { x => x.Vehicle, vehicle },
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<ViewVehicleDetailsDialog>
                (string.Format("View the vehicle", ["Vehicle"]), parameters, options);

            await dialog.Result;
        }

        private async Task OnAddAsync()
        {
            var vehicle = new Vehicle();
            var userId = await AuthenticationService!.GetUserIdAsync();

            var parameters = new DialogParameters<AddVehicleDialog>
            {
                { x => x.Refresh, () => table.ReloadServerData() },
                { x => x.Vehicle, vehicle },
                { x => x.UserProfileId, Guid.Parse(userId!) }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                CloseOnEscapeKey = true
            };

            var dialog = DialogService.Show<AddVehicleDialog>
                (string.Format("Add a new vehicle", ["Vehicle"]), parameters, options);

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
