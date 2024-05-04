using CarCareAlliance.Presentation.Client.Models.ServicePartners;
using CarCareAlliance.Presentation.Client.Models.Tickets;
using CarCareAlliance.Presentation.Client.Models.Vehicles;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CarCareAlliance.Presentation.Client.Models;
using CarCareAlliance.Presentation.Client.Models.Mechanics;

namespace CarCareAlliance.Presentation.Client.Components.Dialogs.UserProfile.UserRepairHistory
{
    public partial class CreateTicketDialog
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [EditorRequired][Parameter] public Ticket Model { get; set; } = default!;
        [Parameter] public Func<Task>? Refresh { get; set; }

        [Inject]
        public IServicePartnerService? ServicePartnerService { get; set; }

        [Inject]
        public IVehicleService? VehicleService { get; set; }

        [Inject]
        public IAuthenticationService? AuthenticationService { get; set; }

        [Inject]
        public IMechanicService? MechanicService { get; set; }

        [Inject]
        public ITicketService? TicketService { get; set; }

        private ICollection<ServicePartner> ServicePartners { get; set; } = [];
        private ICollection<Vehicle> Vehicles { get; set; } = [];
        private ICollection<MechanicProfile> Mechanics { get; set; } = [];

        private MudForm? form;
        private bool isValid;
        private ServicePartner? selectedServicePartner;
        private Vehicle? selectedVehicle;
        private ServiceCategory? selectedServiceCategory;
        private IEnumerable<Service> selectedServices = [];
        private Service? selectedService;
        private MechanicProfile? selectedMechanic;
        private Guid userProfileId;

        protected override async Task OnInitializedAsync()
        {
            var servicePartnersResponse = await ServicePartnerService!.GetAllAsync();
            ServicePartners = servicePartnersResponse.ServicePartners;

            var id = await AuthenticationService!.GetUserIdAsync();

            if (id is not null)
            {
                userProfileId = Guid.Parse(id);

                var queryParams = new QueryParams
                {
                    PageSize = 100,
                    PageNumber = 1,
                };

                var vehiclesResponse = await VehicleService!.GetAllByUserIdAsync(id, queryParams);
                Vehicles = vehiclesResponse.Data;

                var mechanicResponse = await MechanicService!.GetAllByFiltersAsync(queryParams);
                Mechanics = mechanicResponse.Data;
            }

            await base.OnInitializedAsync();
        }

        private void Cancel() => MudDialog.Cancel();

        private async Task Create()
        {
            await form!.Validate().ConfigureAwait(false);

            if (!form!.IsValid)
            {
                return;
            }

            var request = new CreateTicketRequest
            {
                UserProfileId = userProfileId,
                VehicleId = selectedVehicle!.VehicleId,
                Mileage = Model.OrderDetails.Mileage,
                ServicePartnerId = selectedServicePartner!.ServicePartnerId,
                OrderDetailsComments = Model.OrderDetails.Comments,
                TicketDescription = Model.Description,
                ServiceIds = selectedServices.Select(x => x.ServiceId).ToList(),
                AssignedMechanicId = selectedMechanic!.MechanicId
            };

            var isSuccess = await TicketService!.CreateAsync(request);

            if (isSuccess)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }

        private string GetServiceNames(List<string> selectedValues)
        {
            return $"Selected service{(selectedServices.Count() > 1 ? "s" : "")}: {string.Join(", ", selectedServices.Select(x => x.Name))}";
        }
    }
}
